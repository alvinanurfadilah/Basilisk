using System;
using System.ComponentModel.DataAnnotations;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels.Supplier;

namespace BasiliskWeb.Validations;

public class UniqueSupplierCompanyNameValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (BasiliskTFContext?)validationContext.GetService(typeof(BasiliskTFContext)) ?? throw new NullReferenceException("System Error");

            var id = ((SupplierInsertUpdateViewModel)validationContext.ObjectInstance).Id;
            var isExist = dbContext.Suppliers.Any(supp => supp.CompanyName == value.ToString() && supp.Id != id);

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }

        return ValidationResult.Success;
    }
}
