using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;
using iBlogs.Site.Core.Service.Common;
using iBlogs.Site.Core.Service.Content;
using iBlogs.Site.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ApiController : Controller
    {
        private readonly IMetasService _metasService;
        private readonly ISiteService _siteService;
        private readonly IContentsService _contentsService;

        public ApiController(IMetasService metasService, ISiteService siteService, IContentsService contentsService)
        {
            _metasService = metasService;
            _siteService = siteService;
            _contentsService = contentsService;
        }

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
            articleParam.Type=Types.ARTICLE;
            articleParam.Page--;
            Page<Contents> articles = _contentsService.findArticles(articleParam);
            return RestResponse<Page<Contents>>.ok(articles);
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
        [Route("/admin/api/category/save")]
        public RestResponse saveCategory([FromBody]MetaParam metaParam)
        {
            _metasService.saveMeta(Types.CATEGORY, metaParam.cname, metaParam.mid);
            _siteService.cleanCache(Types.SYS_STATISTICS);
            return RestResponse.ok();
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
        public RestResponse CategoryList()
        {
            return RestResponse<List<Metas>>.ok(_siteService.getMetas(Types.RECENT_META,Types.CATEGORY,iBlogsConst.MAX_POSTS));
        }

        // @GetRoute("tags")
        public RestResponse TagList()
        {
            return RestResponse<List<Metas>>.ok(_siteService.getMetas(Types.RECENT_META, Types.TAG, iBlogsConst.MAX_POSTS));
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