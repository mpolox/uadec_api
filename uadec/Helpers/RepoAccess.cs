

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using uadec.DTOs;

namespace uadec.Helpers
{
    public class RepoAccess
    {
        public List<UserModel> GetEntityUsers(DbContext dbContext)
        {

            var parameters = new SqlParameter[] {
                    new SqlParameter("@clientId", 23)
                 };



            return null;
        }
    }
}
