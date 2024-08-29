using Microsoft.AspNetCore.Mvc;
using NumberOrderingApi.Models;
using NumberOrderingApi.Services;

namespace NumberOrderingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberOrderingController : ControllerBase
    {
        private readonly INumberOrderingService _numberOrderingService;
        public NumberOrderingController(INumberOrderingService numberOrderingService)
        {
            _numberOrderingService = numberOrderingService;
        }

        [HttpPost]
        [Route("OrderNumbers")]
        public async Task<IActionResult> OrderNumbers([FromBody] AddNumberOrderingRequest request)
        {
            await _numberOrderingService.SortAndSaveNumbers(request.Numbers);
            return Ok();
        }

        [HttpGet]
        [Route("LoadLatestOrderedNumbers")]
        public async Task<IActionResult> LoadLatestOrderedNumbers()
        {
            var numbers = await _numberOrderingService.GetLastSortedNumbers();
            return Ok(numbers);
        }
    }
}
