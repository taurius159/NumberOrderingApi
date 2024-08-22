using System.ComponentModel.DataAnnotations;
using NumeriuUzsakymasApi.CustomModelValidations;

namespace NumeriuUzsakymasApi.Models
{
    public class AddNumberOrderingRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        [NumberOneToTen]
        public int[] Numbers { get; set; }
    }
}