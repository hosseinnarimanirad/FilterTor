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

public sealed class DeleteByIdsPolyFilterHandler : IRequestHandler<DeleteByIdsPolyFilterCommand, DeleteByIdsPolyFilterResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteByIdsPolyFilterHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteByIdsPolyFilterResponse> Handle(DeleteByIdsPolyFilterCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.PolyFilters.DeleteRangeAsync(request.PolyFilterIds);

        await _unitOfWork.SaveChangesAsync();

        return new DeleteByIdsPolyFilterResponse() { IsSucceed = true };
    }
}
