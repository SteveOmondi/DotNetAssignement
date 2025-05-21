# OrderManagementSystem

## Overview

OrderManagementSystem is a modular .NET-based backend application designed to manage orders (with discounting), customers, and promotions, as well as provide analytical insights such as average order value and fulfillment time. The project utilizes separation of concerns, dependency injection, and repository patterns for maintainability and scalability.

---

## 1. Implementation Approach

- **Domain Driven + Layered Architecture**:  
  The codebase is organized into logical layers/directories:  
    - `Domain`: Contains core business entities and repository interfaces.
    - `Application`: Houses service logic implementing business use cases.
    - `Infrastructure`: Provides data persistence and concrete implementations of repositories.
    - `Controllers`: Exposes RESTful API endpoints for client interaction.
    - `extensions`: Contains extension methods for wiring up services (e.g., dependency injection).
- **ASP.NET Core**:  
  The application is built on ASP.NET Core, leveraging its built-in dependency injection and middleware pipeline.
- **Service Registration**:  
  All domain services and repositories are registered using an extension method (`AddOrderManagementServices`) for clean and centralized configuration.
- **Swagger Integration**:  
  Swagger (OpenAPI) is enabled for easy API documentation and testing in development mode.

---

## 2. Assumptions

- The system interacts with a data store via repository abstractions (concrete implementation details are in `Infrastructure`).
- Business logic is encapsulated in services, not controllers, ensuring testability and separation of concerns.
- Only authenticated or authorized users can access relevant endpoints (authorization middleware is present, though actual policy details are not shown).
- Seasonal promotions are a core feature, and endpoints exist for querying and adding them.
- Analytics endpoints provide aggregated metrics, suggesting the presence of relevant data in the underlying store.
- Authentication and Authorization is handled by other modules not implemented in this sample

---

## 3. Design Patterns

- **Repository Pattern**:  
  Used to abstract data access logic and provide a clean interface for business logic to interact with data sources.
- **Dependency Injection (DI)**:  
  Achieved via `IServiceCollection` extension, promoting loose coupling and testability.
- **Controller-Service Separation**:  
  Controllers act as thin HTTP endpoints, delegating business logic to services.
- **Extension Methods**:  
  Used for modular service registration (e.g., `AddOrderManagementServices`).

---

## 4. Optimizations & Best Practices

- **Centralized Service Registration**:  
  Reduces duplication and improves maintainability.
- **Async/Await**:  
  All controller actions are asynchronous, supporting scalable request handling.
- **Swagger for API Testing**:  
  Enabled in development for rapid validation of endpoints.
- **Separation of Concerns**:  
  Logic is divided between controllers, services, and repositories for clarity and maintainability.
- **Scalability**:  
  Patterns used (DI, async, layered design) make the system easily extensible for new features (e.g., new analytics, order types, or promotions).

---

## Example Code Snippet

Here’s how services and repositories are registered:

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderManagementServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISeasonalPromotionRepository, SeasonalPromotionRepository>();
        services.AddScoped<OrderAnalysisService>();
        return services;
    }
}
```

---

## Getting Started

1. Clone the repository.
2. Run `dotnet restore` and `dotnet build` inside the `OrderManagementSystem` directory.
3. Update `appsettings.json` as needed for your environment.
4. Launch the application: `dotnet run`.
5. Access Swagger UI at `https://localhost:<port>/swagger` for interactive API documentation.

---

## Further Improvements

- Implement authentication and role-based authorization if not already present.
- Add automated tests for controllers and services.
- Introduce caching for analytics endpoints if underlying queries are expensive.
- Consider using MediatR for CQRS if command/query separation becomes necessary.

---
