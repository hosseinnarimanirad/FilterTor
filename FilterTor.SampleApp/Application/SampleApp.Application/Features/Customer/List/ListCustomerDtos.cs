namespace SampleApp.Application.Features;

using MediatR;
using System;
using System.Collections.Generic;
using FilterTor.Conditions;
using FilterTor.Models;

public class ListCustomerQuery : IRequest<ListCustomerResponse>
{
    public PagingModel? Paging { get; set; }

    public List<SortModelVm>? Sorts { get; set; }

    public JsonConditionBase? Filter { get; set; }
}


public class ListCustomerResponse
{
    public List<ListCustomerResponseItem> Items { get; set; }
}

public class ListCustomerResponseItem
{
    public long Id { get; set; }

    public string Name { get; private set; }

    public DateTime RegisteredDate { get; private set; }

    public decimal Credit { get; private set; }
}

