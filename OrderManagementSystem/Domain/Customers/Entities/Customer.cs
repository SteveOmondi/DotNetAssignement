namespace OrderManagementSystem.Domain.Customers.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }

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
    }
}