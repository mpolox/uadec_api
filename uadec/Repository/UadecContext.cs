using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uadec.DTOs;
using uadec.Models;

namespace uadec.Repository
{
    public class UadecContext : DbContext
    {
        public UadecContext(DbContextOptions options) : base(options)
        {
        }

        #region Models
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        #endregion Models


        #region DbQueries
        public DbQuery<UserModel> UserSP { get; set; }
        #endregion DbQueries



    }
}
