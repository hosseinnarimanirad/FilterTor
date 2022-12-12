namespace SampleApp.Core.Entities;

using FilterTor.Conditions;
using FilterTor.Helpers;
using System;

public class PrizeStore : IHasKey<int>, IHasCreateTimeRequired
{
    public int Id { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public required DateTime CreateTime { get; init; }

    public string? InvoiceConditionJson { get; private set; }

    public string? CustomerConditionJson { get; private set; }

    public static PrizeStore Create(
        DateTime startTime,
        DateTime endTime,
        JsonConditionBase? invoiceConditionJson,
        JsonConditionBase? customerConditionJson)
    {
        if (invoiceConditionJson is not null && !invoiceConditionJson.Validate())
            throw new NotImplementedException("PrizeStore > Create");

        if (customerConditionJson is not null && !customerConditionJson.Validate())
            throw new NotImplementedException("PrizeStore > Create");

        return new PrizeStore()
        {
            CreateTime = DateTime.Now,
            StartDate = startTime,
            EndDate = endTime,
            InvoiceConditionJson = invoiceConditionJson?.Serialize(FilterTorHelper.DefaultJsonSerializerOptions),
            CustomerConditionJson = customerConditionJson?.Serialize(FilterTorHelper.DefaultJsonSerializerOptions)
        };
    }
}
