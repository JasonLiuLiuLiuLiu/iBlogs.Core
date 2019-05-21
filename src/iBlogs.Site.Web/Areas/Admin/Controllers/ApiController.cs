using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Extensions;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;
using iBlogs.Site.Core.Service;
using iBlogs.Site.Core.Service.Common;
using iBlogs.Site.Core.Service.Content;
using iBlogs.Site.Core.Service.Users;
using iBlogs.Site.Core.Utils;
using iBlogs.Site.Core.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _env;
        private readonly IAttachService _attachService;

        public ApiController(IMetasService metasService, ISiteService siteService, IContentsService contentsService, IUserService userService, IHostingEnvironment env, IAttachService attachService)
        {
            _metasService = metasService;
            _siteService = siteService;
            _contentsService = contentsService;
            _userService = userService;
            _env = env;
            _attachService = attachService;
        }

        //@GetRoute("logs")
        public RestResponse sysLogs(PageParam pageParam)
        {
            throw new NotImplementedException();
        }

        // @SysLog("删除页面")
        // @PostRoute("page/delete/:cid")
        public RestResponse deletePage(int cid)
        {
            throw new NotImplementedException();
        }
        [AdminApiRoute("articles/{cid}")]
        public RestResponse<Contents> article(string cid)
        {
            Contents contents = _contentsService.getContents(cid);
            contents.Content = "";
            return RestResponse<Contents>.ok(contents);
        }

        [AdminApiRoute("articles/content/{cid}")]
        public RestResponse<string> articleContent(string cid)
        {
            Contents contents = _contentsService.getContents(cid);
            return RestResponse<string>.ok(contents.Content);
        }


        [AdminApiRoute("article/new")]
        public RestResponse<int> newArticle([FromBody]Contents contents)
        {
            var user = _userService.CurrentUsers;
            contents.Type = Types.ARTICLE;
            contents.AuthorId = user.Uid;
            //将点击数设初始化为0
            contents.Hits = 0;
            //将评论数设初始化为0
            contents.CommentsNum = 0;
            if (stringKit.isBlank(contents.Categories))
            {
                contents.Categories = "默认分类";
            }
            var cid = _contentsService.publish(contents);
            _siteService.cleanCache(Types.SYS_STATISTICS);
            return RestResponse<int>.ok(cid);
        }

        //@PostRoute("article/delete/:cid")
        public RestResponse deleteArticle(int cid)
        {
            throw new NotImplementedException();
        }

        [AdminApiRoute("article/update")]
        public RestResponse<int> updateArticle([FromBody]Contents contents)
        {
            if (contents?.Cid == null)
            {
                return RestResponse<int>.fail("缺少参数，请重试");
            }
            contents.Type = Types.ARTICLE;
            var cid = contents.Cid;
            _contentsService.updateArticle(contents);
            return RestResponse<int>.ok(cid.ValueOrDefault());
        }

        // @GetRoute("articles")
        public RestResponse articleList(ArticleParam articleParam)
        {
            articleParam.Type = Types.ARTICLE;
            articleParam.Page--;
            Page<Contents> articles = _contentsService.findArticles(articleParam);
            return RestResponse<Page<Contents>>.ok(articles);
        }

        [AdminApiRoute("pages")]
        public RestResponse<Page<Contents>> pageList(ArticleParam articleParam)
        {
            articleParam.Type=Types.PAGE;
            articleParam.Page--;
            Page<Contents> articles = _contentsService.findArticles(articleParam);
            return RestResponse<Page<Contents>>.ok(articles);
        }

        //@SysLog("发布页面")
        [AdminApiRoute("page/new")]
        public RestResponse newPage([FromBody]Contents contents)
        {

            var users = _userService.CurrentUsers;
            contents.Type=Types.PAGE;
            contents.AllowPing=true;
            contents.AuthorId=users.Uid;
            _contentsService.publish(contents);
            _siteService.cleanCache(Types.SYS_STATISTICS);
            return RestResponse.ok();
        }

        //@SysLog("修改页面")
        [AdminApiRoute("page/update")]
        public RestResponse updatePage([FromBody]Contents contents)
        {
            if (null == contents.Cid)
            {
                return RestResponse.fail("缺少参数，请重试");
            }
            int cid = contents.Cid.ValueOrDefault();
            contents.Type=Types.PAGE;
            _contentsService.updateArticle(contents);
            return RestResponse.ok(cid);
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
        public RestResponse deleteMeta(int mid)
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
        public RestResponse deleteComment(int coid)
        {
            throw new NotImplementedException();
        }

        // @SysLog("修改评论状态")
        // @PostRoute("comment/status")
        public RestResponse updateStatus(Comments comments)
        {
            throw new NotImplementedException();
        }

        // @SysLog("回复评论")
        // @PostRoute("comment/reply")
        public RestResponse replyComment(Comments comments)
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
        public RestResponse deleteAttach(int id)
        {
            throw new NotImplementedException();
        }

        // @GetRoute("categories")
        public RestResponse CategoryList()
        {
            return RestResponse<List<Metas>>.ok(_siteService.getMetas(Types.RECENT_META, Types.CATEGORY, iBlogsConst.MAX_POSTS));
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
        public RestResponse saveTpl(TemplateParam templateParam)
        {
            throw new NotImplementedException();
        }

        /**
        * 上传文件接口
        */
        [AdminApiRoute("attach/upload")]
        public async Task<RestResponse<List<Attach>>> uploadAsync(List<IFormFile> files)
        {

            //log.info("UPLOAD DIR = {}", TaleUtils.UP_DIR);

            var users = _userService.CurrentUsers;
            var uid = users.Uid;
            List<Attach> errorFiles = new List<Attach>();
            List<Attach> urls = new List<Attach>();

            var fileItems = HttpContext.Request.Form.Files;
            if (null == fileItems || fileItems.Count == 0)
            {
                return RestResponse<List<Attach>>.fail("请选择文件上传");
            }

            foreach (var fileItem in fileItems)
            {
                string fname = fileItem.FileName;
                if ((fileItem.Length / 1024) <= iBlogsConst.MAX_FILE_SIZE)
                {
                    var fkey = IBlogsUtils.getFileKey(fname, _env.WebRootPath);

                    var ftype = fileItem.ContentType.Contains("image") ? Types.IMAGE : Types.FILE;
                    var filePath = _env.WebRootPath + fkey;


                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileItem.CopyToAsync(stream);
                    }
                    if (IBlogsUtils.isImage(filePath))
                    {
                        var newFileName = IBlogsUtils.getFileName(fkey);
                        var thumbnailFilePath = _env.WebRootPath + fkey.Replace(newFileName, "thumbnail_" + newFileName);
                        IBlogsUtils.cutCenterImage(_env.WebRootPath + fkey, thumbnailFilePath, 270, 380);

                    }


                    Attach attach = new Attach();
                    attach.FName=fname;
                    attach.AuthorId=uid;
                    attach.FKey=fkey;
                    attach.FType=ftype;
                    attach.Created=DateTime.Now.ToUnixTimestamp();
                    if (await _attachService.Save(attach))
                    {
                        urls.Add(attach);
                        _siteService.cleanCache(Types.SYS_STATISTICS);
                    }
                    else
                    {
                        errorFiles.Add(attach);
                    }
                    
                }
                else
                {
                    Attach attach = new Attach();
                    attach.FName=fname;
                    errorFiles.Add(attach);
                }
            }

            if (errorFiles.Count > 0)
            {
                return RestResponse<List<Attach>>.fail().Payload(errorFiles);
            }
            return RestResponse<List<Attach>>.ok(urls);
        }
    }
}