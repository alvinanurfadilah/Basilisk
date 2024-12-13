using System;
using System.ComponentModel.DataAnnotations;
using BasiliskDataAccess.Models;

namespace BasiliskWeb.Validations;

public class UniqueUsernameValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (BasiliskTFContext?)validationContext.GetService(typeof(BasiliskTFContext)) ?? throw new NullReferenceException("System Error");

            var isExist = dbContext.Accounts.Any(account => account.Username == value.ToString());

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }

        return ValidationResult.Success;
    }
}
