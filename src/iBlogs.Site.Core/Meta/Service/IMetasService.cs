using iBlogs.Site.Core.Content;
using System.Collections.Generic;
using iBlogs.Site.Core.Meta.DTO;

namespace iBlogs.Site.Core.Meta.Service
{
    public interface IMetasService
    {
        TagViewModel LoadTagViewModel();

        List<Metas> getMetas(string type, int limit = 0);

        Dictionary<string, List<Contents>> getMetaMapping(string type);

        Metas getMeta(string type, string name);

        void saveMetas(int? cid, string names, string type);

        void delete(int mid);

        void saveMeta(string type, string name, int? mid);
    }
}