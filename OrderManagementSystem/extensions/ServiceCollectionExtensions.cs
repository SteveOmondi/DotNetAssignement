using OrderManagementSystem.Application.Services;
using OrderManagementSystem.Domain.Customers.Repositories;
using OrderManagementSystem.Domain.Orders.Repositories;
using OrderManagementSystem.Domain.Promotions.Repositories;
using OrderManagementSystem.Infrastructure.Persistence;

namespace OrderManagementSystem.extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderManagementServices(this IServiceCollection services)
        {
            // Register your services and repositories here
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISeasonalPromotionRepository, SeasonalPromotionRepository>();
            services.AddScoped<OrderAnalysisService>();
            return services;
        }
    }
}
