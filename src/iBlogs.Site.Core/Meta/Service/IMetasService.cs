using System.Collections.Generic;
using iBlogs.Site.Core.Meta.DTO;

namespace iBlogs.Site.Core.Meta.Service
{
    public interface IMetasService
    {
        MetaDataViewModel LoadMetaDataViewModel(MetaType type,int topCount=0);
        List<Metas> GetMetas(MetaType type, int limit = 0);
        void SaveMetas(int? cid, string names, MetaType type);
        void Delete(int id);
        void SaveMeta(MetaType type, string name, int? mid);
    }
}