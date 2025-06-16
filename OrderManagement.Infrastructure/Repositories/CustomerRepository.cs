using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities.Customer;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.Infrastructure.Repositories
{
    public class CustomerRepository(OrderManagementDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
    }
}
