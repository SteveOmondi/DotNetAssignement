using OrderManagementSystem.Domain.Promotions.Entities;

namespace OrderManagementSystem.Domain.Promotions.Repositories
{
    public interface ISeasonalPromotionRepository
    {
        Task<SeasonalPromotion?> GetByIdAsync(Guid id);
        Task<List<SeasonalPromotion>> GetActivePromotions();
        Task AddPromotionAsync(SeasonalPromotion promotion);
    }
}