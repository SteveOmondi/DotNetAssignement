using Moq;
using OrderManagementSystem.Application.Services;
using OrderManagementSystem.Domain.Customers.Entities;
using OrderManagementSystem.Domain.Orders.Entities;
using OrderManagementSystem.Domain.Orders.Repositories;
using Xunit;

namespace OrderManagementSystem.Tests.Orders
{
    public class OrderAnalyticsTests
    {
        [Fact]
        public async Task Should_Calculate_AverageOrderValue_WithMockCustomer()
        {
            var mockCustomer = new Customer("John Doe", "john@example.com", new Address("Main St", "City", "Country"));

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Order>
            {
                new Order(mockCustomer.Id, new List<OrderItem> { new OrderItem(Guid.NewGuid(), 1, 100) }),
                new Order(mockCustomer.Id, new List<OrderItem> { new OrderItem(Guid.NewGuid(), 1, 200) })
            });

            var analyticsService = new OrderAnalysisService(mockRepo.Object);
            decimal averageValue = await analyticsService.GetAverageOrderValue();
            Assert.Equal(150, averageValue);
        }
    }

}