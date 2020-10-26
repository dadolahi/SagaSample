using Microsoft.EntityFrameworkCore;
using OrderService.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Core.Abstraction.DbContexts
{
    public interface IOrderServicedbContext
    {
        DbSet<Order> Orders { set; get; }
        DbSet<Product> Products { set; get; }


        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
