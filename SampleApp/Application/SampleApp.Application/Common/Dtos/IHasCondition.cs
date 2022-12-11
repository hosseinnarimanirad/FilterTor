using FilterTor.Conditions;

namespace SampleApp.Application.Common;

public interface IHasCondition
{
    JsonConditionBase Condition { get; }
}
