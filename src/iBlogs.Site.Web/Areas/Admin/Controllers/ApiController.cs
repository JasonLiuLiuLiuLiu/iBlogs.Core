using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using iBlogs.Site.Core.Attach;
using iBlogs.Site.Core.Attach.Service;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Common.Service;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.Content.Service;
using iBlogs.Site.Core.Meta;
using iBlogs.Site.Core.Meta.DTO;
using iBlogs.Site.Core.Meta.Service;
using iBlogs.Site.Core.Option.DTO;
using iBlogs.Site.Core.User.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        public ApiResponse sysLogs(PageParam pageParam)
        {
            throw new NotImplementedException();
        }

        // @SysLog("删除页面")
        // @PostRoute("page/delete/:cid")
        [AdminApiRoute("page/delete/{cid}")]
        public ApiResponse deletePage(int cid)
        {
            _contentsService.delete(cid);
            return ApiResponse.Ok();
        }
        [AdminApiRoute("articles/{cid}")]
        public ApiResponse<Contents> article(string cid)
        {
            Contents contents = _contentsService.getContents(cid);
            contents.Content = "";
            return ApiResponse<Contents>.Ok(contents);
        }

        [AdminApiRoute("articles/content/{cid}")]
        public ApiResponse<string> articleContent(string cid)
        {
            Contents contents = _contentsService.getContents(cid);
            return ApiResponse<string>.Ok(contents.Content);
        }


        [AdminApiRoute("article/new")]
        public ApiResponse<int> newArticle([FromBody]ContentInput contents)
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
            return ApiResponse<int>.Ok(cid, cid);
        }

        //@PostRoute("article/delete/:cid")
        [AdminApiRoute("article/delete/{cid}")]
        public ApiResponse deleteArticle(int cid)
        {
            _contentsService.delete(cid);
            return ApiResponse.Ok();
        }

        [AdminApiRoute("article/update")]
        public ApiResponse<int> updateArticle([FromBody]ContentInput contents)
        {
            if (contents?.Id == null)
            {
                return ApiResponse<int>.Fail("缺少参数，请重试");
            }
            contents.Type = Types.ARTICLE;
            var cid = contents.Id.Value;
            _contentsService.updateArticle(contents);
            return ApiResponse<int>.Ok(cid, cid);
        }

        // @GetRoute("articles")
        public ApiResponse articleList(ArticleParam articleParam)
        {
            articleParam.Type = Types.ARTICLE;
            articleParam.Page--;
            Page<Contents> articles = _contentsService.findArticles(articleParam);
            return ApiResponse<Page<Contents>>.Ok(articles);
        }

        [AdminApiRoute("pages")]
        public ApiResponse<Page<Contents>> pageList(ArticleParam articleParam)
        {
            articleParam.Type = Types.PAGE;
            articleParam.Page--;
            Page<Contents> articles = _contentsService.findArticles(articleParam);
            return ApiResponse<Page<Contents>>.Ok(articles);
        }

        //@SysLog("发布页面")
        [AdminApiRoute("page/new")]
        public ApiResponse newPage([FromBody]ContentInput contents)
        {

            var users = _userService.CurrentUsers;
            contents.Type = Types.PAGE;
            contents.AllowPing = true;
            contents.AuthorId = users.Uid;
            _contentsService.publish(contents);
            _siteService.cleanCache(Types.SYS_STATISTICS);
            return ApiResponse.Ok();
        }

        //@SysLog("修改页面")
        [AdminApiRoute("page/update")]
        public ApiResponse updatePage([FromBody]ContentInput contents)
        {
            if (null == contents.Id)
            {
                return ApiResponse.Fail("缺少参数，请重试");
            }
            int cid = contents.Id.Value;
            contents.Type = Types.PAGE;
            _contentsService.updateArticle(contents);
            return ApiResponse.Ok(cid);
        }

        // @SysLog("保存分类")
        [Route("/admin/api/category/save")]
        public ApiResponse saveCategory([FromBody]MetaParam metaParam)
        {
            _metasService.saveMeta(Types.CATEGORY, metaParam.Cname, metaParam.Id);
            _siteService.cleanCache(Types.SYS_STATISTICS);
            return ApiResponse.Ok();
        }

        // @SysLog("删除分类/标签")
        // @PostRoute()
        [AdminApiRoute("category/delete/{id}")]
        public ApiResponse deleteMeta(int id)
        {
            _metasService.delete(id);
            _siteService.cleanCache(Types.SYS_STATISTICS);
            return ApiResponse.Ok();
        }

        // @GetRoute("comments")
        public ApiResponse commentList(CommentParam commentParam)
        {
            throw new NotImplementedException();
        }

        // @SysLog("删除评论")
        //@PostRoute("comment/delete/:coid")
        public ApiResponse deleteComment(int coid)
        {
            throw new NotImplementedException();
        }

        // @SysLog("修改评论状态")
        // @PostRoute("comment/status")
        public ApiResponse updateStatus(Comments comments)
        {
            throw new NotImplementedException();
        }

        // @SysLog("回复评论")
        // @PostRoute("comment/reply")
        public ApiResponse replyComment(Comments comments)
        {
            throw new NotImplementedException();
        }

        [AdminApiRoute("attaches")]
        public ApiResponse<Page<Attachment>> attachList(PageParam pageParam)
        {
            return ApiResponse<Page<Attachment>>.Ok(_attachService.GetPage(pageParam));
        }

        //    @SysLog("删除附件")
        //@PostRoute("attach/delete/:id")
        [AdminApiRoute("attach/delete/{id}")]
        public ApiResponse deleteAttach(int id)
        {
            _attachService.Delete(id);
            return ApiResponse.Ok();
        }

        // @GetRoute("categories")
        public ApiResponse CategoryList()
        {
            return ApiResponse<List<Metas>>.Ok(_siteService.getMetas(Types.CATEGORY, iBlogsConst.MAX_POSTS));
        }

        // @GetRoute("tags")
        public ApiResponse TagList()
        {
            return ApiResponse<List<Metas>>.Ok(_siteService.getMetas(Types.TAG, iBlogsConst.MAX_POSTS));
        }

        // @GetRoute("options")
        public ApiResponse options()
        {
            throw new NotImplementedException();
        }

        //@SysLog("保存系统配置")
        // @PostRoute("options/save")
        public ApiResponse saveOptions()
        {
            throw new NotImplementedException();
        }

        //@SysLog("保存高级选项设置")
        // @PostRoute("advanced/save")
        public ApiResponse saveAdvance(AdvanceParam advanceParam)
        {
            // 清除缓存
            throw new NotImplementedException();
        }

        //@GetRoute("themes")
        public ApiResponse getThemes()
        {
            throw new NotImplementedException();
        }

        // @SysLog("保存主题设置")
        //@PostRoute("themes/setting")
        public ApiResponse saveSetting()
        {
            throw new NotImplementedException();
        }

        // @SysLog("激活主题")
        // @PostRoute("themes/active")
        public ApiResponse activeTheme()
        {
            throw new NotImplementedException();
        }

        // @SysLog("保存模板")
        // @PostRoute("template/save")
        public ApiResponse saveTpl(TemplateParam templateParam)
        {
            throw new NotImplementedException();
        }

        /**
        * 上传文件接口
        */
        [AdminApiRoute("attach/upload")]
        public async Task<ApiResponse<List<Attachment>>> uploadAsync(List<IFormFile> files)
        {

            //log.info("UPLOAD DIR = {}", TaleUtils.UP_DIR);

            var users = _userService.CurrentUsers;
            var uid = users.Uid;
            List<Attachment> errorFiles = new List<Attachment>();
            List<Attachment> urls = new List<Attachment>();

            var fileItems = HttpContext.Request.Form.Files;
            if (null == fileItems || fileItems.Count == 0)
            {
                return ApiResponse<List<Attachment>>.Fail("请选择文件上传");
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


                    Attachment attachment = new Attachment();
                    attachment.FName = fname;
                    attachment.AuthorId = uid;
                    attachment.FKey = fkey;
                    attachment.FType = ftype;
                    attachment.Created = DateTime.Now.ToUnixTimestamp();
                    if (await _attachService.Save(attachment))
                    {
                        urls.Add(attachment);
                        _siteService.cleanCache(Types.SYS_STATISTICS);
                    }
                    else
                    {
                        errorFiles.Add(attachment);
                    }

                }
                else
                {
                    Attachment attachment = new Attachment();
                    attachment.FName = fname;
                    errorFiles.Add(attachment);
                }
            }

            if (errorFiles.Count > 0)
            {
                return ApiResponse<List<Attachment>>.Fail().SetPayload(errorFiles);
            }
            return ApiResponse<List<Attachment>>.Ok(urls);
        }
    }
}