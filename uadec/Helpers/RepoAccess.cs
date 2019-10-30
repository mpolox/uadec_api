

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


            
            //var entityUsers = dbContext.Database.ExecuteSqlCommand("SELECT * from Students");
                //.SqlQuery<UserModel>("GetUsersByEntity @clientId", parameters)
    //            .ToList();



            //using (EYCDDbContext _context = new EYCDDbContext())
            //{
            //    var parameters = new SqlParameter[] {
            //        new SqlParameter("@clientId", clientId)
            //     };

            //    entityUsers = _context.Database
            //        .SqlQuery<UserAndEntityModel>("GetUsersByEntity @clientId", parameters)
            //        .ToList();
            //}

            return null;
        }
    }
}
