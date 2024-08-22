using System.ComponentModel.DataAnnotations;

namespace NumeriuUzsakymasApi.CustomModelValidations
{
    public class UniqueNumbersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int[] numbers)
            {
                if (numbers.Length != numbers.Distinct().Count())
                {
                    return new ValidationResult("A list of numbers from 1 to 10 allowed, possible to skip some, but none can be repeated");
                }
            }
            return ValidationResult.Success;
        }
    }
}
