namespace SampleApp.Application.Features;

using SampleApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterTor.Conditions;
using SampleApp.Application.Dto;

public class AddPrizeStoreCommand : IRequest<AddPrizeStoreResponse>, IHasCustomerCondition, IHasInvoiceCondition
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public JsonConditionBase? CustomerCondition { get; set; }

    public JsonConditionBase? InvoiceCondition { get; set; }
}

public class AddPrizeStoreResponse
{
    public bool IsSucceed { get; set; } = false;
}

