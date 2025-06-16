using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain.Entities.Customer;
using OrderManagement.Domain.Entities.Order;
using OrderManagement.Domain.Entities.Product;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Infrastructure.Data
{
    public class DataSeeder
    {
        private readonly OrderManagementDbContext _context;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(OrderManagementDbContext context, ILogger<DataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogInformation("Seeding data...");

            if (await _context.Customers.AnyAsync()) return; // Skip if data exists

            var now = DateTime.UtcNow;

            var customer1Id = Guid.Parse("53d43a7b-0808-438d-8a6c-1fc97387ccf5");
            var product1Id = Guid.Parse("308f640d-b82b-4a9a-a51b-f5c98d757777");
            var product2Id = Guid.Parse("8b032067-fa84-4dfd-8450-8f0c6b38a95d");
            var order1Id = Guid.Parse("26ebf892-af6d-4297-b02f-a7ba543503a8");

            var customer = new Customer
            {
                Id = customer1Id,
                Name = "Jason Statam",
                Segment = CustomerSegment.Regular,
                CreatedAt = now,
                CreatedBy = Guid.Empty
            };

            var product1 = new Product
            {
                Id = product1Id,
                Name = "Smartphone",
                Description = "Latest 5G smartphone",
                Price = 599.99m,
                StockQuantity = 50,
                MinStockQuantity = 5,
                CreatedAt = now,
                CreatedBy = Guid.Empty
            };

            var product2 = new Product
            {
                Id = product2Id,
                Name = "Bluetooth Speaker",
                Description = "Portable speaker with deep bass",
                Price = 99.99m,
                StockQuantity = 100,
                MinStockQuantity = 10,
                CreatedAt = now,
                CreatedBy = Guid.Empty
            };

            var order = new Order
            {
                Id = order1Id,
                CustomerId = customer1Id,
                StatusId = OrderStatus.Processing,
                Discount = 50.00m,
                Total = 649.98m,
                CreatedAt = now,
                CreatedBy = Guid.Empty
            };

            var items = new List<OrderItem>
        {
            new()
            {
                Id = Guid.Parse("6a7a049f-19d9-45cd-a100-6dc4afea4026"),
                OrderId = order1Id,
                ProductId = product1Id,
                Quantity = 1,
                Price = 599.99m,
                CreatedAt = now,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.Parse("3283e9ac-7f13-4a6c-b0f5-313f3a3a11e2"),
                OrderId = order1Id,
                ProductId = product2Id,
                Quantity = 1,
                Price = 99.99m,
                CreatedAt = now,
                CreatedBy = Guid.Empty
            }
        };

            var history = new List<OrderStatusHistory>
        {
            new()
            {
                Id = Guid.Parse("2e298e53-e627-4334-b909-775bdf7c5ab7"),
                OrderId = order1Id,
                Name = OrderStatus.Pending,
                CreatedAt = now.AddMinutes(-30),
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.Parse("9e713b2e-4cfe-456b-8edb-549376b82151"),
                OrderId = order1Id,
                Name = OrderStatus.Processing,
                CreatedAt = now,
                CreatedBy = Guid.Empty
            }
        };

            _context.AddRange(customer, product1, product2, order);
            _context.OrderItems.AddRange(items);
            _context.OrderStatus.AddRange(history);



            await _context.SaveChangesAsync();

            _logger.LogInformation("Seeding completed.");
        }
    }
}
