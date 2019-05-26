using Microsoft.EntityFrameworkCore.Storage;

namespace iBlogs.Site.Core.EntityFrameworkCore
{
    public interface ITransactionProvider
    {
        IDbContextTransaction CreateTransaction();
        void Dispose();
    }
}