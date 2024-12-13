using System;
using System.ComponentModel.DataAnnotations;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels.Category;

namespace BasiliskWeb.Validations;

public class UniqueCategoryNameValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (BasiliskTFContext?)validationContext.GetService(typeof(BasiliskTFContext)) ?? throw new NullReferenceException("System Error");

            var id = ((CategoryInsertUpdateViewModel)validationContext.ObjectInstance).Id;
            var isExist = dbContext.Categories.Any(category => category.Name == value.ToString() && category.Id != id);

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }

        return ValidationResult.Success;
    }
}
