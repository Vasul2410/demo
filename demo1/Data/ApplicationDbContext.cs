using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore;
using demo1.model;

namespace demo1.Data
{
    public class ApplicationDbContext : DbContext
    {
        private IConfiguration configuration;
        public DbSet<Person> Person { get; set;}
        public DbSet<product> Product { get; set;}
        public DbSet<Category> Category { get; set;}
        public DbSet<ProductImg> ProductImg { get; set;}
        public DbSet<User> UserTB { get; set; }
        public DbSet<Order> OrderTB { get; set; }
        public DbSet<OrderItem> OrderItemTB { get; set; }


        public ApplicationDbContext(IConfiguration configuration)
        {
            this.configuration=configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var s= configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public DbSet<Person> Persons { get; set; }*/
    }
}
