using System;
using System.ComponentModel.DataAnnotations;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels.Delivery;

namespace BasiliskWeb.Validations;

public class UniqueDeliveryCompanyNameValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (BasiliskTFContext?)validationContext.GetService(typeof(BasiliskTFContext)) ?? throw new NullReferenceException("System Error");

            var id = ((DeliveryInsertUpdateViewModel)validationContext.ObjectInstance).Id;
            var isExist = dbContext.Deliveries.Any(delivery => delivery.CompanyName == value.ToString() && delivery.Id != id);

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }

        return ValidationResult.Success;
    }
}
