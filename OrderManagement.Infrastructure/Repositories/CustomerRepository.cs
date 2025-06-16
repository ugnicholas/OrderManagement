using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities.Customer;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly OrderManagementDbContext _dbContext;
        public CustomerRepository(OrderManagementDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<Customer?> GetByIdWithOrdersAsync(Guid customerId)
        {
            return await _dbContext.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }

    }
}
