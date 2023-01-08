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
using FilterTor.Helpers;

public sealed class GetPrizeStoreHandler : IRequestHandler<GetPrizeStoreQuery, GetPrizeStoreResponse>
{
    public readonly IPrizeStoreQueryRepository _repo;

    public GetPrizeStoreHandler(IPrizeStoreQueryRepository repo)
    {
        _repo = repo;
    }


    public async Task<GetPrizeStoreResponse> Handle(GetPrizeStoreQuery request, CancellationToken cancellationToken)
    {
        var prizeStore = await _repo.GetAsync(request.PrizeStoreId);

        if (prizeStore == null)
            throw new NotImplementedException("GetPrizeStoreHandler > Handle");
         
        return prizeStore.Adapt<GetPrizeStoreResponse>(t =>
        {
            t.InvoiceCondition = JsonConditionBase.Deserialize(prizeStore.InvoiceConditionJson);
            t.CustomerCondition = JsonConditionBase.Deserialize(prizeStore.CustomerConditionJson);
        })!;
    }
}
