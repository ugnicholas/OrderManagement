using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities.Customer;
using OrderManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Repositories
{
    public class CustomerRepository(OrderManagementDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
    }
}
