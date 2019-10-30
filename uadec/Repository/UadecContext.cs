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

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Parent> Parents { get; set; }

        public DbQuery<UserModel> UserSP { get; set; }
    }
}
