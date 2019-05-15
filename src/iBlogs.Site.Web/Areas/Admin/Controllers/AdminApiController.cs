using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminApiController : ControllerBase
    {
        //@GetRoute("logs")
        public RestResponse sysLogs(PageParam pageParam)
        {
            throw new NotImplementedException();
        }

        // @SysLog("删除页面")
        // @PostRoute("page/delete/:cid")
        public RestResponse deletePage( int cid)
        {
            throw new NotImplementedException();
        }

        //@GetRoute("articles/:cid")
        public RestResponse article( string cid)
        {
            throw new NotImplementedException();
        }

        // @GetRoute("articles/content/:cid")
        public void articleContent( string cid)
        {
            throw new NotImplementedException();
        }

        //@PostRoute("article/new")
        public RestResponse newArticle( Contents contents)
        {
            throw new NotImplementedException();
        }

        //@PostRoute("article/delete/:cid")
        public RestResponse deleteArticle( int cid)
        {
            throw new NotImplementedException();
        }

        //@PostRoute("article/update")
        public RestResponse updateArticle( Contents contents)
        {
            throw new NotImplementedException();
        }

        // @GetRoute("articles")
        public RestResponse articleList(ArticleParam articleParam)
        {
            throw new NotImplementedException();
        }

        //@GetRoute("pages")
        public RestResponse pageList(ArticleParam articleParam)
        {
            throw new NotImplementedException();
        }

        //@SysLog("发布页面")
        //@PostRoute("page/new")
        public RestResponse newPage( Contents contents)
        {

            throw new NotImplementedException();
        }

        //@SysLog("修改页面")
        //@PostRoute("page/update")
        public RestResponse updatePage( Contents contents)
        {
            throw new NotImplementedException();
        }

        // @SysLog("保存分类")
        //@PostRoute("category/save")
        public RestResponse saveCategory( MetaParam metaParam)
        {
            throw new NotImplementedException();
        }

        // @SysLog("删除分类/标签")
        // @PostRoute("category/delete/:mid")
        public RestResponse deleteMeta( int mid)
        {
            throw new NotImplementedException();
        }

        // @GetRoute("comments")
        public RestResponse commentList(CommentParam commentParam)
        {
            throw new NotImplementedException();
        }

        // @SysLog("删除评论")
        //@PostRoute("comment/delete/:coid")
        public RestResponse deleteComment( int coid)
        {
            throw new NotImplementedException();
        }

        // @SysLog("修改评论状态")
        // @PostRoute("comment/status")
        public RestResponse updateStatus( Comments comments)
        {
            throw new NotImplementedException();
        }

        // @SysLog("回复评论")
        // @PostRoute("comment/reply")
        public RestResponse replyComment( Comments comments)
        {
            throw new NotImplementedException();
        }

        //  @GetRoute("attaches")
        public RestResponse attachList(PageParam pageParam)
        {

            throw new NotImplementedException();
        }

        //    @SysLog("删除附件")
        //@PostRoute("attach/delete/:id")
        public RestResponse deleteAttach( int id)
        {
            throw new NotImplementedException();
        }

        // @GetRoute("categories")
        public RestResponse categoryList()
        {
            throw new NotImplementedException();
        }

        // @GetRoute("tags")
        public RestResponse tagList()
        {
            throw new NotImplementedException();
        }

        // @GetRoute("options")
        public RestResponse options()
        {
            throw new NotImplementedException();
        }

        //@SysLog("保存系统配置")
        // @PostRoute("options/save")
        public RestResponse saveOptions()
        {
            throw new NotImplementedException();
        }

        //@SysLog("保存高级选项设置")
        // @PostRoute("advanced/save")
        public RestResponse saveAdvance(AdvanceParam advanceParam)
        {
            // 清除缓存
            throw new NotImplementedException();
        }

        //@GetRoute("themes")
        public RestResponse getThemes()
        {
            throw new NotImplementedException();
        }

        // @SysLog("保存主题设置")
        //@PostRoute("themes/setting")
        public RestResponse saveSetting()
        {
            throw new NotImplementedException();
        }

        // @SysLog("激活主题")
        // @PostRoute("themes/active")
        public RestResponse activeTheme()
        {
            throw new NotImplementedException();
        }

        // @SysLog("保存模板")
        // @PostRoute("template/save")
        public RestResponse saveTpl( TemplateParam templateParam)
        {
            throw new NotImplementedException();
        }
    }
}