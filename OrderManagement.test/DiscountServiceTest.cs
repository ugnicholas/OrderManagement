using OrderManagement.Domain.Entities.Customer;
using OrderManagement.Domain.Entities.Order;
using OrderManagement.Domain.Enum;
using OrderManagement.Infrastructure.Services;

namespace OrderManagement.test
{
    public class DiscountServiceTest
    {
        private readonly DiscountService _discountService;

        public DiscountServiceTest()
        {
            _discountService = new DiscountService();
        }

        [Fact]
        public void RegularCustomer_WithLessSpent_NoDiscount()
        {
            // Arrange
            var customer = new Customer
            {
                Segment = CustomerSegment.Regular,
                Orders = new List<Order>
                {
                    new() { Total = 200 },
                    new() { Total = 250 }
                }
            };

            var newOrder = new Order { Total = 100 };

            // Act
            var discount = _discountService.CalculateDiscount(customer, newOrder);

            // Assert
            Assert.Equal(0, discount);
        }

        [Fact]
        public void RegularCustomer_WithMoreSpent_Gets5percentDiscount()
        {
            // Arrange
            var customer = new Customer
            {
                Segment = CustomerSegment.Regular,
                Orders = new List<Order>
                {
                    new() { Total = 500 },
                    new() { Total = 500 }
                }
            };

            var newOrder = new Order { Total = 600 };

            // Act
            var discount = _discountService.CalculateDiscount(customer, newOrder);

            // Assert
            Assert.Equal(30, discount);
        }

        [Fact]
        public void PremiumCustomer_Gets10PercentDiscount()
        {
            // Arrange
            var customer = new Customer
            {
                Segment = CustomerSegment.Premium,
                Orders = new List<Order>
                {
                    new() { Total = 200 }
                }
            };

            var newOrder = new Order { Total = 100 };

            // Act
            var discount = _discountService.CalculateDiscount(customer, newOrder);

            // Assert
            Assert.Equal(10, discount);
        }

        [Fact]
        public void VIPCustomer_Gets15PercentDiscount()
        {
            // Arrange
            var customer = new Customer
            {
                Segment = CustomerSegment.VIP,
                Orders = new List<Order>
                {
                    new() { Total = 200 }
                }
            };

            var newOrder = new Order { Total = 100 };

            // Act
            var discount = _discountService.CalculateDiscount(customer, newOrder);

            // Assert
            Assert.Equal(15, discount);
        }
    }
}