using Microsoft.AspNetCore.Mvc;
using NumeriuUzsakymasApi.Models;
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
        [Route("AddNumberOrdering")]
        public async Task<IActionResult> AddNumberOrdering([FromBody] AddNumberOrderingRequest request)
        {

            _numberOrderingService.SaveSortedNumber(request.Numbers);
            return Ok();
        }
    }
}
