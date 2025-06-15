using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities.Order;
using OrderManagement.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Data
{
    public class OrderManagementDbContext: DbContext
    {
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options): base(options) { }
        
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public DbSet<OrderStatus> OrderStatus => Set<OrderStatus>();
    }
}
