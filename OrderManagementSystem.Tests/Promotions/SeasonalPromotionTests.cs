using OrderManagementSystem.Domain.Promotions.Entities;
using Xunit;

namespace OrderManagementSystem.Tests.Promotions
{
    public class SeasonalPromotionTests
    {
        [Fact]
        public void Promotion_ShouldBeActive_WithinValidDateRange()
        {
            var promotion = new SeasonalPromotion("Holiday Discount", 10, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(5));
            Assert.True(promotion.IsActive());
        }

        [Fact]
        public void Promotion_ShouldNotBeActive_AfterEndDate()
        {
            var promotion = new SeasonalPromotion("Expired Discount", 10, DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddDays(-1));
            Assert.False(promotion.IsActive());
        }

        [Fact]
        public void Promotion_ShouldApplyDiscount_Correctly()
        {
            var promotion = new SeasonalPromotion("Limited Offer", 20, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(5));
            decimal originalPrice = 100;
            decimal discountedPrice = promotion.ApplyPromotion(originalPrice);
            Assert.Equal(80, discountedPrice);
        }
    }
}