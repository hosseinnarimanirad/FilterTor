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
using SampleApp.Core.FilterTor;

public class ListInvoiceQuery : IRequest<ListInvoiceResponse>
{
    [JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
    public EntityType GridKey { get; set; }

    public PagingModel? Paging { get; set; }

    public List<SortModelVm>? Sorts { get; set; }

    public JsonConditionBase? InlineFilter { get; set; }

    public JsonConditionBase? PolyFilter { get; set; }
}


public class ListInvoiceResponse 
{
   

    public List<ListInvoiceResponseItem> Items { get; set; }
}

public class ListInvoiceResponseItem
{
    public long Id { get; set; }

    public Guid RecordId { get; set; }

    /// <summary>
    /// عنوان صندوق
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// نماد معاملاتی
    /// </summary>
    public string TradeCode { get; set; } = null!;

    /// <summary>
    /// اوراق دارد یا نه
    /// </summary>
    public bool HasBond { get; set; }

    /// <summary>
    /// ارزش اوراق
    /// </summary>
    public decimal BondAssetValue { get; set; }

    /// <summary>
    /// ارزش دارایی صندوق درامد ثابت
    /// </summary>
    public decimal InvoiceAssetConstantIncomeValue { get; set; }

    /// <summary>
    /// تاریخ نزدیک
    /// </summary>
    public decimal NearMaturityValue { get; set; }

    /// <summary>
    /// بازده معادل
    /// </summary>
    public double EquivalentYield { get; set; }

    /// <summary>
    /// نقد
    /// </summary>
    public decimal Cash { get; set; }

    /// <summary>
    /// فعال
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// تاریخ آغاز فعالیت
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// حداکثر افزایش موجودی در روز
    /// </summary>
    public decimal? MaxDailyStockIncrease { get; set; }

    /// <summary>
    /// حداقل ارزش معاملات
    /// </summary>
    public decimal? MinTradeValue { get; set; }

    /// <summary>
    /// حداکثر قدرت خرید در روز
    /// </summary>
    public decimal? MaxDailyBuyPower { get; set; }

    /// <summary>
    /// حداکثر کاهش موجودی در روز
    /// </summary>
    public decimal? MaxDailyStockDecrease { get; set; }
}

