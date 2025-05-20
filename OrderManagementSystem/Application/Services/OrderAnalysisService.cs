using OrderManagementSystem.Domain.Orders.Repositories;
using OrderManagementSystem.Domain.Orders.ValueObjects;

namespace OrderManagementSystem.Application.Services
{
    public class OrderAnalysisService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderAnalysisService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<decimal> GetAverageOrderValue()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Any() ? orders.Average(o => o.GetTotalPrice()) : 0;
        }

        public async Task<double> GetAverageFulfillmentTime()
        {
            var orders = await _orderRepository.GetAllAsync();
            var fulfilledOrders = orders.Where(o => o.Status == OrderStatus.Delivered);
            return fulfilledOrders.Any() ? fulfilledOrders.Average(o => (o.OrderDate - DateTime.UtcNow).TotalDays) : 0;
        }
    }
}
