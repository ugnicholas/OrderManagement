using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);   
    }
}
