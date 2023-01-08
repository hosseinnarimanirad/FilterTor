namespace SampleApp.Application.Features;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using SampleApp.Application.Gateways;
using SampleApp.Core.Entities;

public sealed class DeletePrizeStoreHandler : IRequestHandler<DeletePrizeStoreCommand, DeletePrizeStoreResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePrizeStoreHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DeletePrizeStoreResponse> Handle(DeletePrizeStoreCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.PrizeStores.DeleteAsync(request.PrizeStoreId);

        await _unitOfWork.SaveChangesAsync();

        return new DeletePrizeStoreResponse() { IsSucceed = true };
    }
}
