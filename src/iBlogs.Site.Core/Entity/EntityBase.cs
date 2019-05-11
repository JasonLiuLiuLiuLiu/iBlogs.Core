namespace iBlogs.Site.Core.Entity
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public long Created { get; set; }
    }
}
