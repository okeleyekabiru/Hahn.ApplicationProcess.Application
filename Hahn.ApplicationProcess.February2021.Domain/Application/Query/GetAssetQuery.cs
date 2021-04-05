using FluentValidation;
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

namespace Hahn.ApplicationProcess.February2021.Domain.Application.Query
{
    public class GetAssetQuery:IRequest<BaseResponse<Asset>>
    {
        public long Id { get; set; }
    }
   public class GetAssetQueryValidator:AbstractValidator<GetAssetQuery>
    {
        public GetAssetQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }

    }
    public class GetAssetQueryHandler : IRequestHandler<GetAssetQuery, BaseResponse<Asset>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAssetQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<Asset>> Handle(GetAssetQuery request, CancellationToken cancellationToken)
        {
            var asset = _unitOfWork.Assets.Get(request.Id);
            if(asset is null)
                throw new HttpStatusCodeException(StatusCodes.Status404NotFound,"Asset not found ");


            return new BaseResponse<Asset>
            {
                Code = 200,
                 Data = asset,
                 Message = "Asset query successfully"
            };
        }
    }
}
