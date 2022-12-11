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
using FilterTor.Factory;

public sealed class AddPolyFilterHandler : IRequestHandler<AddPolyFilterCommand, AddPolyFilterResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddPolyFilterHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddPolyFilterResponse> Handle(AddPolyFilterCommand request, CancellationToken cancellationToken)
    {
        if (ConditionFactory.IsNotValidOrNull<PolyFilter>(request.Condition, null))
        {
            throw new NotImplementedException("شرط نادرست یا خالی است");
        }

        var polyFilter = PolyFilter.Create(
              request.Condition,
              request.CreatedByFullName,
              request.CreatedById,
              request.IsFavorite,
              request.IsPublic,
              request.Note!,
              request.Title);

        await _unitOfWork.PolyFilters.AddAsync(polyFilter);

        await _unitOfWork.SaveChangesAsync();

        return new AddPolyFilterResponse() { IsSucceed = true };
    }
}
