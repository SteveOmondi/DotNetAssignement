using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Promotions.Entities;
using OrderManagementSystem.Domain.Promotions.Repositories;

namespace OrderManagementSystem.Infrastructure.Persistence
{
    public class SeasonalPromotionRepository : ISeasonalPromotionRepository
    {
        private readonly OrderDbContext _context;

        public SeasonalPromotionRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<SeasonalPromotion?> GetByIdAsync(Guid id)
        {
            return await _context.SeasonalPromotions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<SeasonalPromotion>> GetActivePromotions()
        {
            return await _context.SeasonalPromotions
                                 .Where(p => p.IsActive())
                                 .ToListAsync();
        }

        public async Task AddPromotionAsync(SeasonalPromotion promotion)
        {
            await _context.SeasonalPromotions.AddAsync(promotion);
            await _context.SaveChangesAsync();
        }
    }
}