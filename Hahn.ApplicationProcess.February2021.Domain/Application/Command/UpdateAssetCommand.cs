using Hahn.ApplicationProcess.February2021.Domain.Enums;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Hahn.ApplicationProcess.February2021.Domain.interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Hahn.ApplicationProcess.February2021.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Application.Command
{
    public class UpdateAssetCommand:IRequest<BaseResponse<Asset>>
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public Department? Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAdressOfDepartment { get; set; }
        public bool? Broken { get; set; }
        public DateTimeOffset? PurchaseDate { get; set; }
    }
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, BaseResponse<Asset>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAssetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<Asset>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = _unitOfWork.Assets.Get(request.Id);
            if(asset is null)
                throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "Asset not found");

            asset.PurchaseDate = request.PurchaseDate ?? asset.PurchaseDate;
            asset.CountryOfDepartment = request.CountryOfDepartment ?? asset.CountryOfDepartment;
            asset.Department = request.Department ?? asset.Department;
            asset.AssetName = request.AssetName ?? asset.AssetName;
            asset.Broken = request.Broken ?? asset.Broken;
            asset.EMailAdressOfDepartment = request.EMailAdressOfDepartment ?? asset.EMailAdressOfDepartment;

            _unitOfWork.Assets.Update(asset);
            if(await _unitOfWork.Complete())
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, "An error occured while updating asset");

            }
            return new BaseResponse<Asset> { Code = 200, Data = asset, Message = "Asset successfully updated" };
        }
    }
}
