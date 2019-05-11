using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;

namespace iBlogs.Site.Core.Service.Comment
{
    /**
     * 评论Service
     *
     * @author biezhi
     * @since 1.3.1
     */

    public class CommentsService
    {

        /**
         * 保存评论
         *
         * @param comments
         */
        public void saveComment(Comments comments)
        {


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
        public Page<Comments> getComments(int cid, int page, int limit)
        {
            return null;
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
        private void getChildren(List<Comments> list, int coid)
        {

        }

        private Comments apply(Comments parent)
        {
            return null;
        }

        public Page<Comments> findComments(CommentParam commentParam)
        {
            return null;
        }

    }
}
