using System.Collections.Generic;
using iBlogs.Site.Application.Entity;

namespace iBlogs.Site.Application.Service.Common
{
    public interface IMetasService
    {
        List<Metas> getMetas(string type);
        Dictionary<string, List<Entity.Contents>> getMetaMapping(string type);
        Metas getMeta(string type, string name);
        void saveMetas(int cid, string names, string type);
        void delete(int mid);
        void saveMeta(string type, string name, int mid);
    }
}