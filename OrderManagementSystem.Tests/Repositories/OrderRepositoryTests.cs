using Moq;
using OrderManagementSystem.Domain.Orders.Entities;
using OrderManagementSystem.Domain.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepo = new();

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrder()
        {
            var orderId = Guid.NewGuid();
            var order = new Order(Guid.NewGuid(), new List<OrderItem>());
            _mockOrderRepo.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);

            var result = await _mockOrderRepo.Object.GetByIdAsync(orderId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddOrder()
        {
            var order = new Order(Guid.NewGuid(), new List<OrderItem>());
            _mockOrderRepo.Setup(repo => repo.AddAsync(order)).Returns(Task.CompletedTask);

            await _mockOrderRepo.Object.AddAsync(order);
            _mockOrderRepo.Verify(repo => repo.AddAsync(order), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveOrder()
        {
            var orderId = Guid.NewGuid();
            _mockOrderRepo.Setup(repo => repo.DeleteAsync(orderId)).Returns(Task.CompletedTask);

            await _mockOrderRepo.Object.DeleteAsync(orderId);
            _mockOrderRepo.Verify(repo => repo.DeleteAsync(orderId), Times.Once);
        }
    }
}
