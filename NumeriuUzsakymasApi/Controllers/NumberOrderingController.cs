using Microsoft.AspNetCore.Mvc;
using NumeriuUzsakymasApi.Services;

namespace NumeriuUzsakymasApi.Controllers
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
        [Route("SortAndSave")]
        public async Task<IActionResult> SortAndSave(int[] numbers)
        {
            _numberOrderingService.SaveSortedNumber(numbers);
            return Ok();
        }
    }
}
