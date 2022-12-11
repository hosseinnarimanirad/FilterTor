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
using FilterTor.Conditions;
using SampleApp.Application.Gateways.Repositories;

public sealed class GetPolyFilterHandler : IRequestHandler<GetPolyFilterQuery, GetPolyFilterResponse>
{
    public readonly IPolyFilterQueryRepository _repo;

    public GetPolyFilterHandler(IPolyFilterQueryRepository repo)
    {
        _repo = repo;
    }


    public async Task<GetPolyFilterResponse> Handle(GetPolyFilterQuery request, CancellationToken cancellationToken)
    {
        var polyFilter = await _repo.GetAsync(request.PolyFilterId);

        if (polyFilter == null)
            throw new NotImplementedException("موجودیت یافت نشد");

        //return new GetPolyFilterResponse()
        //{
        //    Id = polyFilter.Id,
        //    ConditionJson = polyFilter.ConditionJson,
        //    CreatedByFullName = polyFilter.CreatedByFullName,
        //    CreatedById = polyFilter.CreatedById,
        //    CreateTime = polyFilter.CreateTime,
        //    IsFavorite = polyFilter.IsFavorite,
        //    IsPublic = polyFilter.IsPublic,
        //    Note = polyFilter.Note,
        //    Title = polyFilter.Title,
        //}; 

        return polyFilter.Adapt<GetPolyFilterResponse>(t => t.Condition = JsonConditionBase.Deserialize(polyFilter.ConditionJson, null));
    }
}
