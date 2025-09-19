using GroceryManager.Models.Dtos.Revenue;
using GroceryManager.Services.Revenues;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevenueController : ControllerBase
    {
        private readonly IRevenuesService _revenuesService;

        public RevenueController(IRevenuesService revenuesService)
        {
            _revenuesService = revenuesService;
        }

        [HttpGet]
        public async Task<List<GetRevenuesDto>> GetAllRevenues()
        {
            var revenues = await _revenuesService.GetAllRevenuesDbAsync();

            return revenues;
        }

        [HttpGet("{name}")]
        public async Task<List<GetRevenuesDto>?> GetRevenueByName(string name)
        {
            var revenue = await _revenuesService.GetRevenuesByNameAsync(name);

            return revenue;
        }
    }
}