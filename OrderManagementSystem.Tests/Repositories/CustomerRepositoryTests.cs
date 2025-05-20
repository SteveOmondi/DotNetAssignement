using Moq;
using OrderManagementSystem.Domain.Customers.Entities;
using OrderManagementSystem.Domain.Customers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Tests.Repositories
{
    public class CustomerRepositoryTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepo = new();

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer()
        {
            var customerId = Guid.NewGuid();
            var customer = new Customer("Jane Doe", "jane@example.com", new Address("Street", "City", "Country"));
            _mockCustomerRepo.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync(customer);

            var result = await _mockCustomerRepo.Object.GetByIdAsync(customerId);
            Assert.NotNull(result);
            Assert.Equal("Jane Doe", result.Name);
        }

        [Fact]
        public async Task AddAsync_ShouldAddCustomer()
        {
            var customer = new Customer("John Doe", "john@example.com", new Address("Main St", "Town", "Country"));
            _mockCustomerRepo.Setup(repo => repo.AddAsync(customer)).Returns(Task.CompletedTask);

            await _mockCustomerRepo.Object.AddAsync(customer);
            _mockCustomerRepo.Verify(repo => repo.AddAsync(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveCustomer()
        {
            var customerId = Guid.NewGuid();
            _mockCustomerRepo.Setup(repo => repo.DeleteAsync(customerId)).Returns(Task.CompletedTask);

            await _mockCustomerRepo.Object.DeleteAsync(customerId);
            _mockCustomerRepo.Verify(repo => repo.DeleteAsync(customerId), Times.Once);
        }
    }
}
