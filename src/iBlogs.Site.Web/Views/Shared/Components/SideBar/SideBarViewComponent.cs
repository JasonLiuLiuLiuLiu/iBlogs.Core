using iBlogs.Site.Core.Meta;
using iBlogs.Site.Core.Meta.Service;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Views.Shared.Components.SideBar
{
    public class SideBarViewComponent:ViewComponent
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
                int.Parse(_optionService.Get(OptionKeys.SideBarTagsCount, "10")));
            var categories= _metasService.LoadMetaDataViewModel(MetaType.Category,
                int.Parse(_optionService.Get(OptionKeys.SideBarCategoriesCount, "10")));
            return View(new SideBarViewModel {Tags = tags.Data, Categories = categories.Data});
        }
    }
}
