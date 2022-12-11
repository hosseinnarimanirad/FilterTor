namespace SampleApp.Application.Features;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using SampleApp.Application.Gateways;
using SampleApp.Application.Extensions;
using SampleApp.Core.Entities; 
using Mapster;
using SampleApp.Application.Gateways.Repositories;
using FilterTor.Extensions;
using FilterTor.Conditions;

public sealed class ListByIdsPolyFilterHandler : IRequestHandler<ListByIdsPolyFilterQuery, ListByIdsPolyFilterResponse>
{
    public readonly IPolyFilterQueryRepository _repo;

    public ListByIdsPolyFilterHandler(IPolyFilterQueryRepository repo)
    {
        _repo = repo;
    }


    public async Task<ListByIdsPolyFilterResponse> Handle(ListByIdsPolyFilterQuery request, CancellationToken cancellationToken)
    {
        var polyFilters = await _repo.GetAllByIdsAsync(request.PolyFilterIds);

        if (polyFilters.IsNullOrEmpty())
            throw new NotImplementedException("موجودیت یافت نشد");

        return new ListByIdsPolyFilterResponse()
        {
            Items = polyFilters.Select(p => p.Adapt<ListByIdsPolyFilterItem>(t => t.Condition = JsonConditionBase.Deserialize(p.ConditionJson, null))).ToList()
        };
    }
}
