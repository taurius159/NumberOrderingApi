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
        [Route("AddNumberOrdering")]
        public async Task<IActionResult> AddNumberOrdering([FromBody] AddNumberOrderingRequest request)
        {

            _numberOrderingService.SaveSortedNumber(request.Numbers);
            return Ok();
        }
    }
}
