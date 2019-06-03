using System.Collections.Generic;
using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.EntityFrameworkCore;
using System.Linq;
using AutoMapper;

namespace iBlogs.Site.Core.Comment.Service
{
    /**
     * 评论Service
     *
     * @author biezhi
     * @since 1.3.1
     */

    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comments> _repository;
        private readonly IMapper _mapper;

        public CommentsService(IRepository<Comments> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
        }

        /**
         * 删除评论，暂时没用
         *
         * @param coid
         * @param cid
         * @throws Exception
         */
        public void delete(int coid, int cid)
        {


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
            var query = _repository.GetAll().Where(c => c.Cid == param.Cid);
            return _mapper.Map<Page<CommentResponse>>(_repository.Page(query, param));
        }

        /**
         * 获取文章下的评论统计
         *
         * @param cid 文章ID
         */
        public long getCommentCount(int cid)
        {
            return 0;
        }

        /**
         * 获取该评论下的追加评论
         *
         * @param coid
         * @return
         */
        private void GetChildren(List<Comments> list, int coid)
        {

        }


        public Page<Comments> findComments(CommentParam commentParam)
        {
            return null;
        }

    }
}
