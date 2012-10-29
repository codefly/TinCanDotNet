using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;

namespace TinCanDotNet.Model
{
    public class Actor
    {
        public string objectType { get; set; }
        public string name { get; set; }
        public string mbox { get; set; }
        public string mbox_sha1sum { get; set; }
        public string openid { get; set; }
        public Account account { get; set; }

        public long Insert(SqlConnection cn)
        {
            long? acc_id = null;
            if(account != null)
                acc_id = account.Insert(cn);
            string sql = @"
                insert into Agent(objectType, name, mbox, mbox_sha1sum, openid, AccountID)
                   values(@objectType, @name, @mbox, @mbox_sha1sum, @openid, @AccountID);
                select cast(Scope_identity() as bigint);";
            var result =  cn.Query<long>(sql, new
            {
                objectType = this.objectType ?? "Actor",
                name = this.name,
                mbox = this.mbox,
                mbox_sha1sum = this.mbox_sha1sum,
                openid = this.openid,
                AccountID = acc_id
            });
            return result.Single();
            

        }
    }
}
