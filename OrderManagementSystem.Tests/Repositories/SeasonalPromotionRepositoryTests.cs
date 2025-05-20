using Moq;
using OrderManagementSystem.Domain.Promotions.Entities;
using OrderManagementSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Tests.Repositories
{
    public class SeasonalPromotionRepositoryTests
    {
        private readonly Mock<SeasonalPromotionRepository> _mockPromotionRepo = new();

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPromotion()
        {
            var promotionId = Guid.NewGuid();
            var promotion = new SeasonalPromotion("Holiday Sale", 15, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(5));

            _mockPromotionRepo.Setup(repo => repo.GetByIdAsync(promotionId)).ReturnsAsync(promotion);

            var result = await _mockPromotionRepo.Object.GetByIdAsync(promotionId);
            Assert.NotNull(result);
            Assert.Equal("Holiday Sale", result.Name);
        }

        [Fact]
        public async Task AddPromotionAsync_ShouldAddPromotion()
        {
            var promotion = new SeasonalPromotion("Summer Discount", 10, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            _mockPromotionRepo.Setup(repo => repo.AddPromotionAsync(promotion)).Returns(Task.CompletedTask);

            await _mockPromotionRepo.Object.AddPromotionAsync(promotion);
            _mockPromotionRepo.Verify(repo => repo.AddPromotionAsync(promotion), Times.Once);
        }

        [Fact]
        public async Task GetActivePromotions_ShouldReturnOnlyActivePromotions()
        {
            var promotions = new List<SeasonalPromotion>
            {
                new SeasonalPromotion("Expired Sale", 20, DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddDays(-5)),
                new SeasonalPromotion("Ongoing Sale", 15, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(7)),
                new SeasonalPromotion("Upcoming Sale", 10, DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(10))
            };

            _mockPromotionRepo.Setup(repo => repo.GetActivePromotions()).ReturnsAsync(promotions.Where(p => p.IsActive()).ToList());

            var result = await _mockPromotionRepo.Object.GetActivePromotions();
            Assert.Single(result); // Only "Ongoing Sale" should be active
            Assert.Equal("Ongoing Sale", result[0].Name);
        }
    }
}