using System.Collections.Generic;
using iBlogs.Site.Core.Comment;

namespace iBlogs.Site.Core.Meta.Service
{
    public interface IMetasService
    {
        List<Metas> getMetas(string type);
        Dictionary<string, List<Contents>> getMetaMapping(string type);
        Metas getMeta(string type, string name);
        void saveMetas(int? cid, string names, string type);
        void delete(int mid);
        void saveMeta(string type, string name, int? mid);
    }
}