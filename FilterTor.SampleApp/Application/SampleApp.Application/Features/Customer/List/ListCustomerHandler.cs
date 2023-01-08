namespace SampleApp.Application.Features;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using SampleApp.Core.Entities;
using SampleApp.Application.Extensions;
using Mapster;
using SampleApp.Application.Gateways.Repositories;
using FilterTor.Models;
using FilterTor.Helpers;
using FilterTor.Conditions;
using SampleApp.FilterTorEx;

public sealed class ListCustomerHandler : IRequestHandler<ListCustomerQuery, ListCustomerResponse>
{
    private readonly ICustomerQueryRepository _repo;

    public ListCustomerHandler(ICustomerQueryRepository repo)
    {
        _repo = repo;
    }

    public async Task<ListCustomerResponse> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
    {
        var sorts = request.Sorts?.Select(s => s.Adapt<SortModel>(sm => { sm.Entity = EntityType.Customer.ToString(); })!)?.ToList();

        var items = await _repo.Filter(request.Filter, sorts, request.Paging);

        return new ListCustomerResponse() { Items = items.Select(i => i.Adapt<ListCustomerResponseItem>()).ToList() };
    }
}
