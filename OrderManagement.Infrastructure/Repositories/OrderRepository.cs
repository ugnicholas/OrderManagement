using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities.Order;
using OrderManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly OrderManagementDbContext _dbContext;
        public OrderRepository(OrderManagementDbContext context):base(context)
        {
            _dbContext = context;
        }

        // Custom method only for Order
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                .Include(o => o.StatusHistory)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
