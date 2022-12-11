namespace FilterTor;

// COMPOSITE DESIGN PATTERN:
// Component
public interface ICondition
{
    CategoryType Category { get; }

    bool IsPassed(IConditionParameter parameter);         
}
