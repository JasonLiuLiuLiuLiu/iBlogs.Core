using System;
using System.Linq;
using AutoMapper;
using iBlogs.Site.Core.Blog.Comment.DTO;
using iBlogs.Site.Core.Blog.Content.Service;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.MailKit;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.Security.Service;
using LibGit2Sharp;

namespace iBlogs.Site.Core.Blog.Comment.Service
{

    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comments> _repository;
        private readonly IContentsService _contentsService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IOptionService _optionService;
        private readonly IMailService _mailService;

        public CommentsService(IRepository<Comments> repository, IMapper mapper, IContentsService contentsService, IUserService userService, IOptionService optionService, IMailService mailService)
        {
            _repository = repository;
            _mapper = mapper;
            _contentsService = contentsService;
            _userService = userService;
            _optionService = optionService;
            _mailService = mailService;
        }

        public int GetTotalCount()
        {
            return _repository.GetAll().Count();
        }
        /**
         * 保存评论
         *
         * @param comments
         */
        public void SaveComment(Comments comments)
        {
            if (!ConfigData.Get(ConfigKey.AllowCommentAudit, "true").ToBool())
                comments.Status = CommentStatus.Approved;

            _repository.InsertOrUpdate(comments);
            _repository.SaveChanges();
            _contentsService.UpdateCommentCount(comments.Cid, 1);


            _optionService.Set(ConfigKey.CommentCount, _repository.GetAll().Where(u => u.Status == CommentStatus.Approved).Select(u => u.Id).Count().ToString());

        }

        public void Reply(Comments comments)
        {
            var replyComment = _repository.FirstOrDefault(comments.Parent);
            if (replyComment == null)
                throw new NotFoundException("没有找到该评论");

            if (_userService.CurrentUsers == null)
                throw new Exception("该接口仅提供给管理员调用,请登录");

            comments.Status = CommentStatus.Approved;
            comments.Cid = replyComment.Cid;
            comments.Author = _userService.CurrentUsers.Username;
            comments.Mail = _userService.CurrentUsers.Email;
            comments.Url = ConfigData.Get(ConfigKey.SiteUrl);

            _repository.InsertOrUpdate(comments);
            _repository.SaveChanges();
            _contentsService.UpdateCommentCount(comments.Cid, 1);

            _optionService.Set(ConfigKey.CommentCount, _repository.GetAll().Where(u => u.Status == CommentStatus.Approved).Select(u => u.Id).Count().ToString());
            _mailService.Publish(new MailContext
            {
                To =new []{ replyComment.Mail },
                Content = $"亲爱的{replyComment.Author},您的评论:{replyComment.Content},有了新的回复:{comments.Content}"
            });
        }

        /**
         * 删除评论，暂时没用
         *
         * @param coid
         * @param cid
         * @throws Exception
         */
        public void Delete(int? id)
        {
            if (!id.HasValue)
                throw new Exception("没找到该评论!");
            _repository.Delete(id.Value);
            _repository.SaveChanges();
        }

        public void UpdateComment(CommentParam param)
        {
            var comment = _repository.FirstOrDefault(param.Coid);
            if (comment == null)
                throw new Exception("没找到该评论!");
            comment.Status = param.Status;

            _repository.Update(comment);

            _optionService.Set(ConfigKey.CommentCount, _repository.GetAll().Where(u => u.Status == CommentStatus.Approved).Select(u => u.Id).Count().ToString());

            _repository.SaveChanges();

            if (param.Status == CommentStatus.Approved)
            {
                _mailService.Publish(new MailContext
                {
                    To = new[] { comment.Mail },
                    Content = $"亲爱的{comment.Author},您的评论已审核通过:{comment.Content}"
                });
            }
        }

        /**
         * 获取文章下的评论
         *
         * @param cid
         * @param page
         * @param limit
         * @return
         */
        public Page<CommentResponse> GetComments(CommentPageParam param)
        {
            var query = _repository.GetAll();
            if (param.Status.HasValue)
                query = query.Where(c => c.Status == param.Status);

            if (param.Cid.HasValue)
                query = query.Where(c => c.Cid == param.Cid);

            return _mapper.Map<Page<CommentResponse>>(_repository.Page(query, param));
        }


    }
}
