using OrderManagement.Application.Interfaces;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly OrderManagementDbContext _context;

        public IOrderRepository Orders { get; }
        public ICustomerRepository Customers { get; }

        public UnitOfWork(OrderManagementDbContext context,
                          IOrderRepository orderRepository,
                          ICustomerRepository customerRepository)
        {
            _context = context;
            Orders = orderRepository;
            Customers = customerRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
