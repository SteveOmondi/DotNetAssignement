using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Domain.Promotions.Entities;
using OrderManagementSystem.Infrastructure.Persistence;

namespace OrderManagementSystem.Controllers
{

    [ApiController]
    [Route("api/promotions")]
    public class SeasonalPromotionController : ControllerBase
    {
        private readonly SeasonalPromotionRepository _promotionRepository;

        public SeasonalPromotionController(SeasonalPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActivePromotions()
        {
            var promotions = await _promotionRepository.GetActivePromotions();
            return Ok(promotions);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPromotion([FromBody] SeasonalPromotion promotion)
        {
            await _promotionRepository.AddPromotionAsync(promotion);
            return Ok(new { Message = "Promotion added successfully!" });
        }
    }
}

