using System;
using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using iBlogs.Site.Core.Content.Service;

namespace iBlogs.Site.Core.Comment.Service
{
    
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comments> _repository;
        private readonly IContentsService _contentsService;
        private readonly IMapper _mapper;

        public CommentsService(IRepository<Comments> repository, IMapper mapper, IContentsService contentsService)
        {
            _repository = repository;
            _mapper = mapper;
            _contentsService = contentsService;
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
            _repository.InsertOrUpdate(comments);
            _repository.SaveChanges();
            _contentsService.UpdateCommentCount(comments.Cid, 1);
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
            if(!id.HasValue)
                throw new Exception("没找到该评论!");
            _repository.Delete(id.Value);
            _repository.SaveChanges();
        }

        public void UpdateComment(CommentParam param)
        {
            var comment=_repository.FirstOrDefault(param.Coid);
            if(comment==null)
                throw new Exception("没找到该评论!");
            comment.Status = param.Status;
            _repository.Update(comment);
            _repository.SaveChanges();
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
            var query = _repository.GetAll()
                .Where(c => c.Status == param.Status);

            if (param.Cid.HasValue)
                query = query.Where(c => c.Cid == param.Cid);

            return _mapper.Map<Page<CommentResponse>>(_repository.Page(query, param));
        }


    }
}
