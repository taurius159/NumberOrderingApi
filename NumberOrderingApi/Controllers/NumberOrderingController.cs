using System.ComponentModel.DataAnnotations;
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
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var validationResult = await _numberOrderingService.SortAndSaveNumbers(request.Numbers);

            if (validationResult != ValidationResult.Success)
            {
                return UnprocessableEntity(validationResult.ErrorMessage);
            }

            return Ok("Numbers sorted and saved successfully.");
        }

        [HttpGet]
        [Route("LoadContentOfLatestSavedFile")]
        public async Task<IActionResult> LoadContentOfLatestSavedFile()
        {
            var numbers = await _numberOrderingService.LoadContentOfLatestSavedFile();

            if (numbers == string.Empty)
            {
                return NotFound("No records found.");
            }

            return Ok(numbers);
        }
    }
}
