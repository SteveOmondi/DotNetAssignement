namespace OrderManagementSystem.Domain.Orders.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<OrderItem> Items { get; private set; } = new();
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Guid customerId, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Items = items;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }

        public void MarkAsShipped()
        {
            Status = OrderStatus.Shipped;
        }

        public void Cancel()
        {
            Status = OrderStatus.Cancelled;
        }
    }
}