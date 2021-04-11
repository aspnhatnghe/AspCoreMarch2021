using System;
using System.ComponentModel.DataAnnotations;

namespace D08_Validation.Models
{
    internal class CheckBirthDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var dateValue = DateTime.Parse(value.ToString());
            var dateValue = Convert.ToDateTime(value);

            // >= 10 tuoi mới hợp lệ
            if (DateTime.Now.Year - dateValue.Year >= 10)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Chưa đủ tuổi");
        }
    }
}