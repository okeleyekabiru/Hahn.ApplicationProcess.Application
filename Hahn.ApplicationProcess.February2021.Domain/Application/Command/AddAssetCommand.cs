using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.Enums;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Hahn.ApplicationProcess.February2021.Domain.interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Hahn.ApplicationProcess.February2021.Domain.Response;
using Hahn.ApplicationProcess.February2021.Domain.Validators;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Application.Command
{
    public class AddAssetCommand:IRequest<BaseResponse<Unit>>
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public Department Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAdressOfDepartment { get; set; }
        public bool Broken { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
    }

    public class AddAssetCommandValidator : AbstractValidator<AddAssetCommand>
    {
        public AddAssetCommandValidator()
        {
            RuleFor(x => x.AssetName).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.Department).NotEmpty().IsInEnum();
            RuleFor(x => x.PurchaseDate).GreaterThan(DateTimeOffset.Now.AddDays(-new DateTime(DateTimeOffset.Now.Year, 12, 31).DayOfYear));
            RuleFor(x => x.EMailAdressOfDepartment).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Broken).NotNull();
            RuleFor(x => x.CountryOfDepartment).NotNull().NotEmpty().SetValidator(new CountryValidator(new HttpClient()));
        }
    }

    public class AddAssetCommandHandler : IRequestHandler<AddAssetCommand, BaseResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddAssetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<Unit>> Handle(AddAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = new Asset 
            {
            AssetName = request.AssetName,
             Broken = request.Broken,
             CountryOfDepartment = request.CountryOfDepartment,
             Department = request.Department,
             EMailAdressOfDepartment = request.EMailAdressOfDepartment,
             PurchaseDate = request.PurchaseDate
            };

            _unitOfWork.Assets.Add(asset);
            if(!await _unitOfWork.Complete())
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, "An Error Occured while adding asset");
            }

            return new BaseResponse<Unit> { Code = 201, Data = Unit.Value, Message = "Asset successfully created" };
        }
    }

}
