using OrderManagementSystem.Domain.Customers.ValueObjects;
using OrderManagementSystem.Domain.Orders.Entities;

namespace OrderManagementSystem.Domain.Customers.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        public List<Order> OrderHistory { get; private set; } = new();


        public Customer(string name, string email, Address address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Address = address;
        }

        public void UpdateAddress(Address newAddress)
        {
            Address = newAddress;
        }

        public CustomerSegment GetSegment()
        {
            if (OrderHistory.Count == 0) return CustomerSegment.NewCustomer;
            if (OrderHistory.Count >= 5) return CustomerSegment.LoyalCustomer;
            if (OrderHistory.Sum(o => o.GetTotalPrice()) > 5000) return CustomerSegment.HighSpendingCustomer;
            return CustomerSegment.LoyalCustomer;
        }

    }
}