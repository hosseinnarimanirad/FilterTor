namespace SampleApp.Application.Common.Mapster;

using global::Mapster;
using SampleApp.Core.Entities;
using FilterTor.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConditionProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<PolyFilter, IHasCondition>()
        //     .Map(destination => destination.Condition.Serialize(),
        //            source => JsonConditionBase.Deserialize(source.ConditionJson, null));

        config.NewConfig<PolyFilter, IHasCondition>()
           .Map(destination => destination.Condition,
                  source => JsonConditionBase.Deserialize(source.ConditionJson, null));
    }
}
