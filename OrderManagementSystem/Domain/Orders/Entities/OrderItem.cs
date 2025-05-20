namespace OrderManagementSystem.Domain.Orders.Entities
{
    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        private decimal _discountedPrice;


        public OrderItem(Guid productId, int quantity, decimal price)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            _discountedPrice = price;
        }

        public void ApplyDiscount(decimal percentage)
        {
            _discountedPrice = Price - (Price * (percentage / 100));
        }

        public decimal GetTotalPrice() => _discountedPrice * Quantity;

    }
}