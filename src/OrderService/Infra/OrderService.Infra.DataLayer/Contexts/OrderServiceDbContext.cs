using Microsoft.EntityFrameworkCore;
using OrderService.Core.Abstraction.DbContexts;
using OrderService.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Infra.DataLayer.Contexts
{
    public class OrderServiceDbContext : DbContext,IOrderServicedbContext
    {
        public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new List<Product>
            {
                new Product { Id=1, Title = $"Product {1}" },
                new Product { Id=2, Title = $"Product {2}" },
                new Product { Id=3, Title = $"Product {3}" },
                new Product { Id=4, Title = $"Product {4}" },
                new Product { Id=6, Title = $"Product {6}" },
                new Product { Id=5, Title = $"Product {5}" },
                new Product { Id=7, Title = $"Product {7}" },
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { set; get; }
        public DbSet<Product> Products { set; get; }
    }
}
