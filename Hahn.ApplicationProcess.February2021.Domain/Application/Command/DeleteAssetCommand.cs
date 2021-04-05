using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Hahn.ApplicationProcess.February2021.Domain.interfaces;
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
    public class DeleteAssetCommand:IRequest<BaseResponse<Unit>>
    {
        public long Id { get; set; }
    }

    public class DeleteAssetCommandyValidator : AbstractValidator<DeleteAssetCommand>
    {
        public DeleteAssetCommandyValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();

        }
      

    }


    public class DeleteCommandHandler : IRequestHandler<DeleteAssetCommand, BaseResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<Unit>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = _unitOfWork.Assets.Get(request.Id);
            if (asset is null)
                throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "Asset not found");



            _unitOfWork.Assets.Remove(asset);
            if (await _unitOfWork.Complete())
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, "An error occured while updating asset");

            }
            return new BaseResponse<Unit> { Code = 200, Data = Unit.Value, Message = "Asset successfully Deleted" };
        }
    }
}
