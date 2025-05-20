namespace OrderManagementSystem.Domain.Promotions.Entities
{
    public class SeasonalPromotion
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public SeasonalPromotion(string name, decimal discountPercentage, DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            DiscountPercentage = discountPercentage;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsActive()
        {
            return DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
        }

        public decimal ApplyPromotion(decimal originalPrice)
        {
            return IsActive() ? originalPrice - (originalPrice * (DiscountPercentage / 100)) : originalPrice;
        }
    }
}
