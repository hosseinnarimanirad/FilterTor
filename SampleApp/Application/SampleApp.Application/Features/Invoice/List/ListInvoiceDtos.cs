namespace SampleApp.Application.Features;

using MediatR;
using System;
using System.Collections.Generic;
using FilterTor.Conditions;
using FilterTor.Models;
using SampleApp.Core;

public class ListInvoiceQuery : IRequest<ListInvoiceResponse>
{
    public PagingModel? Paging { get; set; }

    public List<SortModelVm>? Sorts { get; set; }

    public JsonConditionBase? Filter { get; set; }
}


public class ListInvoiceResponse
{
    public List<ListInvoiceResponseItem> Items { get; set; }
}

public class ListInvoiceResponseItem
{
    public long Id { get; set; }

    public DateTime CreateTime { get; set; }

    public string InvoiceNumber { get; set; }

    public DateTime InvoiceDate { get; set; }

    public bool IsSettled { get; set; }

    public decimal TotalAmount { get; set; }

    public long CustomerId { get; set; }

    public InvoiceType InvoiceType { get; set; }
}

