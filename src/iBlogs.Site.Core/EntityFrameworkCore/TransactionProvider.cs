using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace iBlogs.Site.Core.EntityFrameworkCore
{
    public class TransactionProvider:IDisposable, ITransactionProvider
    {
        private readonly iBlogsContext _blogsContext;

        public TransactionProvider(iBlogsContext blogsContext)
        {
            _blogsContext = blogsContext;
        }

        public List<IDbContextTransaction> Transactions=new List<IDbContextTransaction>();

        public IDbContextTransaction CreateTransaction()
        {
            var tra=_blogsContext.Database.BeginTransaction();
            Transactions.Add(tra);
            return tra;
        }

        public void Dispose()
        {
            for (var i = Transactions.Count-1; i >= 0; i--)
            {
                Transactions[i].Dispose();
            }
        }
    }
}
