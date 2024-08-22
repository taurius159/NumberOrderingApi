using System.ComponentModel.DataAnnotations;

namespace NumeriuUzsakymasApi.CustomModelValidations
{
    public class NumberOneToTenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int[] numbers)
            {
                foreach (var number in numbers)
                {
                    if (number < 1 || number > 10)
                    {
                        return new ValidationResult("Each of the passed numbers must be between 1 and 10");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
