namespace SampleApp.Application.Features;

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

    
}

