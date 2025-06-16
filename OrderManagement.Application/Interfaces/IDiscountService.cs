using OrderManagement.Domain.Entities.Customer;
using OrderManagement.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Interfaces
{
    public interface IDiscountService
    {
        decimal CalculateDiscount(Customer customer, Order order);
    }
}
