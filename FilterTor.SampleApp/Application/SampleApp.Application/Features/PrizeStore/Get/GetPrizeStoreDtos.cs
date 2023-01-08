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

public class GetPrizeStoreQuery : IRequest<GetPrizeStoreResponse>
{
    public int PrizeStoreId { get; set; }
}

public class GetPrizeStoreResponse : IHasInvoiceCondition, IHasCustomerCondition
{
    public int Id { get; set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public DateTime? CreateTime { get; set; }
     
    public JsonConditionBase? InvoiceCondition { get; set; }

    public JsonConditionBase? CustomerCondition { get; set; }

}

