using iBlogs.Site.Core.Attach;
using iBlogs.Site.Core.Attach.Service;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Common.Service;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.Content.Service;
using iBlogs.Site.Core.Log;
using iBlogs.Site.Core.Meta;
using iBlogs.Site.Core.Meta.DTO;
using iBlogs.Site.Core.Meta.Service;
using iBlogs.Site.Core.Option.DTO;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.User.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
        private readonly IOptionService _optionService;

        public ApiController(IMetasService metasService, ISiteService siteService, IContentsService contentsService, IUserService userService, IHostingEnvironment env, IAttachService attachService, IOptionService optionService)
        {
            _metasService = metasService;
            _siteService = siteService;
            _contentsService = contentsService;
            _userService = userService;
            _env = env;
            _attachService = attachService;
            _optionService = optionService;
        }

        [AdminApiRoute("logs")]
        public ApiResponse<List<Logs>> SysLogs(PageParam pageParam)
        {
            return ApiResponse<List<Logs>>.Ok(new List<Logs>());
        }

        // @SysLog("删除页面")
        // @PostRoute("page/delete/:cid")
        [AdminApiRoute("page/delete/{cid}")]
        public ApiResponse DeletePage(int cid)
        {
            _contentsService.delete(cid);
            return ApiResponse.Ok();
        }

        [AdminApiRoute("articles/{cid}")]
        public ApiResponse<Contents> Article(string cid)
        {
            Contents contents = _contentsService.getContents(cid);
            contents.Content = "";
            return ApiResponse<Contents>.Ok(contents);
        }

        [AdminApiRoute("articles/content/{cid}")]
        public ApiResponse<string> ArticleContent(string cid)
        {
            Contents contents = _contentsService.getContents(cid);
            return ApiResponse<string>.Ok(contents.Content);
        }

        [AdminApiRoute("article/new")]
        public ApiResponse<int> NewArticle([FromBody]ContentInput contents)
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
            return ApiResponse<int>.Ok(cid, cid);
        }

        //@PostRoute("article/delete/:cid")
        [AdminApiRoute("article/delete/{cid}")]
        public ApiResponse DeleteArticle(int cid)
        {
            _contentsService.delete(cid);
            return ApiResponse.Ok();
        }

        [AdminApiRoute("article/update")]
        public ApiResponse<int> UpdateArticle([FromBody]ContentInput contents)
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
        public ApiResponse ArticleList(ArticleParam articleParam)
        {
            articleParam.Type = Types.ARTICLE;
            articleParam.Page--;
            Page<Contents> articles = _contentsService.findArticles(articleParam);
            return ApiResponse<Page<Contents>>.Ok(articles);
        }

        [AdminApiRoute("pages")]
        public ApiResponse<Page<Contents>> PageList(ArticleParam articleParam)
        {
            articleParam.Type = Types.PAGE;
            articleParam.Page--;
            Page<Contents> articles = _contentsService.findArticles(articleParam);
            return ApiResponse<Page<Contents>>.Ok(articles);
        }

        //@SysLog("发布页面")
        [AdminApiRoute("page/new")]
        public ApiResponse NewPage([FromBody]ContentInput contents)
        {
            var users = _userService.CurrentUsers;
            contents.Type = Types.PAGE;
            contents.AllowPing = true;
            contents.AuthorId = users.Uid;
            _contentsService.publish(contents);
            return ApiResponse.Ok();
        }

        //@SysLog("修改页面")
        [AdminApiRoute("page/update")]
        public ApiResponse UpdatePage([FromBody]ContentInput contents)
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
        public ApiResponse SaveCategory([FromBody]MetaParam metaParam)
        {
            _metasService.saveMeta(Types.CATEGORY, metaParam.Cname, metaParam.Id);
            return ApiResponse.Ok();
        }

        // @SysLog("删除分类/标签")
        // @PostRoute()
        [AdminApiRoute("category/delete/{id}")]
        public ApiResponse DeleteMeta(int id)
        {
            _metasService.delete(id);
            return ApiResponse.Ok();
        }

        [AdminApiRoute("comments")]
        public ApiResponse<List<Comments>> CommentList(CommentParam commentParam)
        {
            return ApiResponse<List<Comments>>.Ok(new List<Comments>());
        }

        // @SysLog("删除评论")
        //@PostRoute("comment/delete/:coid")
        public ApiResponse DeleteComment(int coid)
        {
            throw new NotImplementedException();
        }

        // @SysLog("修改评论状态")
        // @PostRoute("comment/status")
        public ApiResponse UpdateStatus(Comments comments)
        {
            throw new NotImplementedException();
        }

        // @SysLog("回复评论")
        // @PostRoute("comment/reply")
        public ApiResponse ReplyComment(Comments comments)
        {
            throw new NotImplementedException();
        }

        [AdminApiRoute("attaches")]
        public ApiResponse<Page<Attachment>> AttachList(PageParam pageParam)
        {
            return ApiResponse<Page<Attachment>>.Ok(_attachService.GetPage(pageParam));
        }

        //    @SysLog("删除附件")
        //@PostRoute("attach/delete/:id")
        [AdminApiRoute("attach/delete/{id}")]
        public ApiResponse DeleteAttach(int id)
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
        [AdminApiRoute("options")]
        public ApiResponse<IDictionary<string, string>> Options()
        {
            return ApiResponse<IDictionary<string, string>>.Ok(_optionService.GetAll());
        }

        //@SysLog("保存系统配置")
        [AdminApiRoute("options/save")]
        public ApiResponse SaveOptions(IDictionary<string, string> options)
        {
            _optionService.SaveOptions(options);
            return ApiResponse.Ok();
        }

        //@SysLog("保存高级选项设置")
        // @PostRoute("advanced/save")
        [AdminApiRoute("advanced/save")]
        public ApiResponse SaveAdvance(AdvanceParam advanceParam)
        {
            // 要过过滤的黑名单列表
            if (!advanceParam.BlockIps.IsNullOrWhiteSpace())
            {
                _optionService.saveOption(Types.BLOCK_IPS, advanceParam.BlockIps);
            }
            else
            {
                _optionService.saveOption(Types.BLOCK_IPS, "");
            }

            if (!advanceParam.CdnUrl.IsNullOrWhiteSpace())
            {
                _optionService.saveOption(iBlogsConst.OPTION_CDN_URL, advanceParam.CdnUrl);
            }

            // 是否允许重新安装
            if (!advanceParam.AllowInstall.IsNullOrWhiteSpace())
            {
                _optionService.saveOption(iBlogsConst.OPTION_ALLOW_INSTALL, advanceParam.AllowInstall);
            }

            // 评论是否需要审核
            if (!advanceParam.AllowCommentAudit.IsNullOrWhiteSpace())
            {
                _optionService.saveOption(iBlogsConst.OPTION_ALLOW_COMMENT_AUDIT, advanceParam.AllowCommentAudit);
            }

            // 是否允许公共资源CDN
            if (!advanceParam.AllowCloudCdn.IsNullOrWhiteSpace())
            {
                _optionService.saveOption(iBlogsConst.OPTION_ALLOW_CLOUD_CDN, advanceParam.AllowCloudCdn);
            }
            return ApiResponse.Ok();
        }

        /**
        * 上传文件接口
        */

        [AdminApiRoute("attach/upload")]
        public async Task<ApiResponse<List<Attachment>>> UploadAsync(List<IFormFile> files)
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