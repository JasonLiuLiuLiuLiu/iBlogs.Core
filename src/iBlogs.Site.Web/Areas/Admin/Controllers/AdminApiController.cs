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
        public RestResponse<?> deletePage(@PathParam Integer cid)
        {
            throw new NotImplementedException();
        }

        //@GetRoute("articles/:cid")
        public RestResponse article(@PathParam String cid)
        {
            throw new NotImplementedException();
        }

        // @GetRoute("articles/content/:cid")
        public void articleContent(@PathParam String cid, Response response)
        {
            throw new NotImplementedException();
        }

        //@PostRoute("article/new")
        public RestResponse newArticle(@BodyParam Contents contents)
        {
            throw new NotImplementedException();
        }

        //@PostRoute("article/delete/:cid")
        public RestResponse<?> deleteArticle(@PathParam Integer cid)
        {
            throw new NotImplementedException();
        }

        //@PostRoute("article/update")
        public RestResponse updateArticle(@BodyParam Contents contents)
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
        public RestResponse<?> newPage(@BodyParam Contents contents)
        {

            throw new NotImplementedException();
        }

        //@SysLog("修改页面")
        //@PostRoute("page/update")
        public RestResponse<?> updatePage(@BodyParam Contents contents)
        {
            throw new NotImplementedException();
        }

        // @SysLog("保存分类")
        //@PostRoute("category/save")
        public RestResponse<?> saveCategory(@BodyParam MetaParam metaParam)
        {
            throw new NotImplementedException();
        }

        // @SysLog("删除分类/标签")
        // @PostRoute("category/delete/:mid")
        public RestResponse<?> deleteMeta(@PathParam Integer mid)
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
        public RestResponse<?> deleteComment(@PathParam Integer coid)
        {
            throw new NotImplementedException();
        }

        // @SysLog("修改评论状态")
        // @PostRoute("comment/status")
        public RestResponse<?> updateStatus(@BodyParam Comments comments)
        {
            throw new NotImplementedException();
        }

        // @SysLog("回复评论")
        // @PostRoute("comment/reply")
        public RestResponse<?> replyComment(@BodyParam Comments comments, Request request)
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
        public RestResponse<?> deleteAttach(@PathParam Integer id)
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
        public RestResponse<?> saveOptions(Request request)
        {
            throw new NotImplementedException();
        }

        //@SysLog("保存高级选项设置")
        // @PostRoute("advanced/save")
        public RestResponse<?> saveAdvance(AdvanceParam advanceParam)
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
        public RestResponse<?> saveSetting(Request request)
        {
            throw new NotImplementedException();
        }

        // @SysLog("激活主题")
        // @PostRoute("themes/active")
        public RestResponse<?> activeTheme(@BodyParam ThemeParam themeParam)
        {
            throw new NotImplementedException();
        }

        // @SysLog("保存模板")
        // @PostRoute("template/save")
        public RestResponse<?> saveTpl(@BodyParam TemplateParam templateParam)
        {
            throw new NotImplementedException();
        }
    }
}