using Microsoft.EntityFrameworkCore;
using OutOfSchool.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OutOfSchool.DAL
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Organization> Organizations { get; set; }
    }
}
