using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Dataaccess
{
    public class DBcont : DbContext
    {
        public DBcont(DbContextOptions<DBcont> options) : base(options)
        {
        }
        public DbSet<Department> departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
  }
