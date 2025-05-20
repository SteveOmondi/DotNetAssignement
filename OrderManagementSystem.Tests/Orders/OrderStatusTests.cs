using OrderManagementSystem.Application.Services;
using OrderManagementSystem.Domain.Customers.Entities;
using OrderManagementSystem.Domain.Orders.Entities;
using OrderManagementSystem.Domain.Orders.ValueObjects;
using Xunit;

namespace OrderManagementSystem.Tests.Orders
{
    public class OrderStatusTests
    {
        private readonly Customer _mockCustomer = new("Jane Doe", "jane@example.com", new Address("Oak St", "Town", "Country"));


        [Fact]
        public void Order_ShouldStartAsPending()
        {
            var order = new Order(_mockCustomer.Id, new List<OrderItem>());
            Assert.Equal(OrderStatus.Pending, order.Status);
        }

        [Fact]
        public void Order_ShouldTransitionFromPendingToProcessing()
        {
            var order = new Order(_mockCustomer.Id, new List<OrderItem>());
            order.ProcessOrder();
            Assert.Equal(OrderStatus.Processing, order.Status);
        }

        [Fact]
        public void Order_ShouldTransitionFromProcessingToShipped()
        {
            var order = new Order(_mockCustomer.Id, new List<OrderItem>());
            order.ProcessOrder();
            order.MarkAsShipped();
            Assert.Equal(OrderStatus.Shipped, order.Status);
        }

        [Fact]
        public void Order_ShouldNotShip_IfNotProcessed()
        {
            var order = new Order(_mockCustomer.Id, new List<OrderItem>());
            order.MarkAsShipped();
            Assert.NotEqual(OrderStatus.Shipped, order.Status);
        }
    }

}