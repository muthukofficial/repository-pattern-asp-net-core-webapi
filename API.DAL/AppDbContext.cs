using System;
using API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
