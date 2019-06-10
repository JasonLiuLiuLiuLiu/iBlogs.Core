using iBlogs.Site.Core.Content;
using System.Collections.Generic;
using iBlogs.Site.Core.Meta.DTO;

namespace iBlogs.Site.Core.Meta.Service
{
    public interface IMetasService
    {
        MetaDataViewModel LoadMetaDataViewModel(string type);

        List<Metas> GetMetas(string type, int limit = 0);

        Dictionary<string, List<Contents>> GetMetaMapping(string type);

        Metas GetMeta(string type, string name);

        void SaveMetas(int? cid, string names, string type);

        void Delete(int mid);

        void SaveMeta(string type, string name, int? mid);
    }
}