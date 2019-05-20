using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.SqLite;

namespace iBlogs.Site.Core.Service
{
    public class AttachService : IAttachService
    {
        private readonly IDbConnection _sqlite;

        public AttachService(IDbBaseRepository sqlite)
        {
            _sqlite = sqlite.DbConnection();
        }

        public async Task<bool> Save(Attach attach)
        {
            if(attach==null)
                throw new NullReferenceException();

            var  result=await  _sqlite.ExecuteAsync(
                "insert into t_attach (fname,ftype,fkey,author_id,created) values (@FName,@FType,@FKey,@AuthorId,@Created)",
                attach);
            return result == 1;
        }
    }
}
