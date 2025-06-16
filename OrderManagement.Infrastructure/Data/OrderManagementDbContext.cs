using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Common;
using OrderManagement.Domain.Entities.Customer;
using OrderManagement.Domain.Entities.Order;
using OrderManagement.Domain.Entities.Product;

namespace OrderManagement.Infrastructure.Data
{
    public class OrderManagementDbContext : DbContext
    {
        private readonly IUserContext _userContext;

        public OrderManagementDbContext(
            DbContextOptions<OrderManagementDbContext> options,
            IUserContext userContext)
            : base(options)
        {
            _userContext = userContext;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatusHistory> OrderStatus { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;
                var userId = _userContext.UserId;

                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.Id == Guid.Empty)
                            entry.Entity.Id = Guid.NewGuid();

                        entry.Entity.CreatedAt = now;
                        entry.Entity.CreatedBy = userId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        entry.Entity.UpdatedBy = userId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderManagementDbContext).Assembly);
        }
    }

}
