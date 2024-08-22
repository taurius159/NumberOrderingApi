using System.ComponentModel.DataAnnotations;
using NumberOrderingApi.CustomModelValidations;

namespace NumberOrderingApi.Models
{
    public class AddNumberOrderingRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        [NumberOneToTen]
        [UniqueNumbers]
        public int[] Numbers { get; set; }
    }
}