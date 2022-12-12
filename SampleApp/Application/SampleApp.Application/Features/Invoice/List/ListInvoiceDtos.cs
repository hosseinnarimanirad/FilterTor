namespace SampleApp.Application.Features.Invoice.List;

using SampleApp.Application.Common; 
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks; 
using SampleApp.Application.Features;
using FilterTor.Common.Converters;
using FilterTor.Conditions;
using FilterTor.Models;
using SampleApp.FilterTorEx;

public class ListInvoiceQuery : IRequest<ListInvoiceResponse>
{
    [JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
    public EntityType GridKey { get; set; }

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

    
}

