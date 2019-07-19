namespace iBlogs.Site.Core.Blog.Relationship.Service
{
    public interface IRelationshipService
    {
        void SaveOrUpdate(int cid, int mid);

        void DeleteByContentId(int cid);

        void DeleteByMetaId(int id);

        int GetContentCountByMetaDataId(int mid);
    }
}