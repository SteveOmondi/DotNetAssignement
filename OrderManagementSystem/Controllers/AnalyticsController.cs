using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Services;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly OrderAnalysisService _analyticsService;

        public AnalyticsController(OrderAnalysisService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("average-order-value")]
        public async Task<IActionResult> GetAverageOrderValue()
        {
            var value = await _analyticsService.GetAverageOrderValue();
            return Ok(new { AverageOrderValue = value });
        }

        [HttpGet("average-fulfillment-time")]
        public async Task<IActionResult> GetAverageFulfillmentTime()
        {
            var time = await _analyticsService.GetAverageFulfillmentTime();
            return Ok(new { AverageFulfillmentTime = time });
        }
    }
}


