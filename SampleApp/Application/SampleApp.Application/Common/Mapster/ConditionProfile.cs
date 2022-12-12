namespace SampleApp.Application.Common.Mapster;

using global::Mapster;
using SampleApp.Core.Entities;
using FilterTor.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterTor.Helpers;
using SampleApp.Application.Dto;

public class ConditionProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PrizeStore, IHasCustomerCondition>()
                .Map(destination => destination.CustomerCondition,
                        source => JsonConditionBase.Deserialize(source.CustomerConditionJson));

        config.NewConfig<PrizeStore, IHasInvoiceCondition>()
                .Map(destination => destination.InvoiceCondition,
                        source => JsonConditionBase.Deserialize(source.InvoiceConditionJson));
    }
}
