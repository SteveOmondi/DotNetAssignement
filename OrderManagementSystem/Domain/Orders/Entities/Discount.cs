
using OrderManagementSystem.Domain.Orders.ValueObjects;

namespace OrderManagementSystem.Domain.Orders.Entities
{

    public class Discount
    {
        public Guid Id { get; private set; }
        public decimal Percentage { get; private set; }
        public DateTime ValidUntil { get; private set; }
        public DiscountType Type { get; private set; }

        public Discount(decimal percentage, DateTime validUntil, DiscountType type)
        {
            Id = Guid.NewGuid();
            Percentage = percentage;
            ValidUntil = validUntil;
            Type = type;
        }

        public bool IsValid() => DateTime.UtcNow <= ValidUntil;

        public decimal ApplyDiscount(decimal originalPrice)
        {
            if (!IsValid()) return originalPrice; // No discount if expired
            return originalPrice - (originalPrice * (Percentage / 100));
        }
    }
}

