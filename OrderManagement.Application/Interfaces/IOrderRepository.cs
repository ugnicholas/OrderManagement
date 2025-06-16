using OrderManagement.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<List<Order>> GetAllWithStatusHistoryAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);

        Task<Order?> GetByIdWithStatusHistoryAsync(Guid orderId);
    }
}
