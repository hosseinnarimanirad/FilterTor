namespace SampleApp.Application.Features.Invoice.List;

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

public sealed class ListInvoiceHandler : IRequestHandler<ListInvoiceQuery, ListInvoiceResponse>
{
    private readonly IInvoiceQueryRepository _repo;

    public ListInvoiceHandler(IInvoiceQueryRepository repo)
    {
        _repo = repo;
    }

    public async Task<ListInvoiceResponse> Handle(ListInvoiceQuery request, CancellationToken cancellationToken)
    {
        var sorts = request.Sorts?.Select(s => s.Adapt<SortModel>(sm => { sm.Entity = request.GridKey.ToString(); })!)?.ToList();

        var items = await _repo.Filter(request.Filter, sorts, request.Paging);

        return new ListInvoiceResponse() { Items = items.Select(i => i.Adapt<ListInvoiceResponseItem>()).ToList() };
    }
}
