using System;
using System.Globalization;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Meta.Service;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Views.Shared.Components.SideBar
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IMetasService _metasService;
        private readonly IOptionService _optionService;

        public SideBarViewComponent(IMetasService metasService, IOptionService optionService)
        {
            _metasService = metasService;
            _optionService = optionService;
        }

        public IViewComponentResult Invoke()
        {
            var tags = _metasService.LoadMetaDataViewModel(MetaType.Tag,
                int.Parse(_optionService.Get(ConfigKey.SideBarTagsCount, "10")));
            var categories = _metasService.LoadMetaDataViewModel(MetaType.Category,
                int.Parse(_optionService.Get(ConfigKey.SideBarCategoriesCount, "10")));
            return View(new SideBarViewModel
            {
                Tags = tags.Data,
                Categories = categories.Data,
                ContentCount = _optionService.Get(ConfigKey.ContentCount,"0"),
                CommentCount = _optionService.Get(ConfigKey.CommentCount,"0"),
                RunTime = DateTime.Parse(_optionService.Get(ConfigKey.SiteInstallTime,DateTime.Now.ToString(CultureInfo.InvariantCulture))).DateDiffToNow(),
                LastActiveTime = DateTime.Parse(_optionService.Get(ConfigKey.LastActiveTime, DateTime.Now.ToString(CultureInfo.InvariantCulture))).DateDiffToNow(),
            });
        }
    }
}
