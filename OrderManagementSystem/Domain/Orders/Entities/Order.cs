using OrderManagementSystem.Domain.Orders.ValueObjects;
using OrderManagementSystem.Domain.Promotions.Entities;

namespace OrderManagementSystem.Domain.Orders.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<OrderItem> Items { get; private set; } = new();
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public Discount? AppliedDiscount { get; private set; }
        public SeasonalPromotion? AppliedPromotion { get; private set; }



        public Order(Guid customerId, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Items = items;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }

        public void ApplySeasonalPromotion(SeasonalPromotion promotion)
        {
            if (promotion.IsActive())
            {
                AppliedPromotion = promotion;
                foreach (var item in Items)
                {
                    item.ApplyDiscount(promotion.DiscountPercentage);
                }
            }
        }

        public void ApplyDiscount(Discount discount)
        {
            if (discount.IsValid())
            {
                AppliedDiscount = discount;
                foreach (var item in Items)
                {
                    item.ApplyDiscount(discount.Percentage);
                }
            }
        }
        public decimal GetTotalPrice()
        {
            decimal total = Items.Sum(i => i.GetTotalPrice());
            return AppliedDiscount != null ? AppliedDiscount.ApplyDiscount(total) : total;
        }


        public void ProcessOrder()
        {
            if (Status == OrderStatus.Pending)
                Status = OrderStatus.Processing;
        }

        public void MarkAsShipped()
        {
            if (Status == OrderStatus.Processing)
                Status = OrderStatus.Shipped;
        }

        public void DeliverOrder()
        {
            if (Status == OrderStatus.Shipped)
                Status = OrderStatus.Delivered;
        }


        public void Cancel()
        {
            if (Status == OrderStatus.Pending || Status == OrderStatus.Processing)
                Status = OrderStatus.Cancelled;
        }
    }
}