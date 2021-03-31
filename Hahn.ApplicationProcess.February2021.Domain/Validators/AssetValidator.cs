using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Hahn.ApplicationProcess.February2021.Domain.Validators
{
    public class AssetValidator:AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(x => x.AssetName).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.Department).NotEmpty().IsInEnum();
            RuleFor(x => x.PurchaseDate).GreaterThan(DateTimeOffset.Now.AddDays(-new DateTime(DateTimeOffset.Now.Year, 12, 31).DayOfYear));
            RuleFor(x => x.EMailAdressOfDepartment).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Broken).NotNull();
            RuleFor(x => x.CountryOfDepartment).NotNull().NotEmpty().SetValidator(new CountryValidator(new HttpClient()));
        }
    }
}
