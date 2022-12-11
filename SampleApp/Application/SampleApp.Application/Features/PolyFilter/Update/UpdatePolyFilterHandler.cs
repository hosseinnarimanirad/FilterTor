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

public sealed class UpdatePolyFilterHandler : IRequestHandler<UpdatePolyFilterCommand, UpdatePolyFilterResponse>
{
    public readonly IUnitOfWork _unitOfWork;

    public UpdatePolyFilterHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdatePolyFilterResponse> Handle(UpdatePolyFilterCommand request, CancellationToken cancellationToken)
    {
        var polyFilter = await _unitOfWork.PolyFilters.GetAsync(request.PolyFilterId);

        if (polyFilter == null)
            throw new NotImplementedException();

        polyFilter.Update(request.Title, request.Note);

        await _unitOfWork.SaveChangesAsync();

        return new UpdatePolyFilterResponse()
        {
            IsSucceed = true,
        };
    }
}
