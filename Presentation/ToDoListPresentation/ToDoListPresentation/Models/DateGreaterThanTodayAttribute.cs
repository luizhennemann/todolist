using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListPresentation.Models
{
    public class DateGreaterThanTodayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            DateTime date = (DateTime)value;

            if (date >= DateTime.Now.Date)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Date must be equal or greater than today.");
        }
    }
}
