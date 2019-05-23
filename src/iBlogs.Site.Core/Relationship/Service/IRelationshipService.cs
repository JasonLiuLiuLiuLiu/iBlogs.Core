namespace iBlogs.Site.Core.Relationship.Service
{
    public interface IRelationshipService
    {
        void SaveOrUpdate(int cid,int mid);
        void DeleteByContentId(int cid);
    }
}