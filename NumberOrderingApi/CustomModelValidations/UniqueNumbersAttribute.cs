using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.CustomModelValidations
{
    public class UniqueNumbersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int[] numbers)
            {
                if (numbers.Length != numbers.Distinct().Count())
                {
                    return new ValidationResult("None of the numbers can be repeated.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
