using GridEngineCore; 

namespace GridEngine
{
    // COMPOSITE DESIGN PATTERN:
    // Component
    public interface ICondition
    {
        CategoryType Category { get; }

        bool IsPassed(IConditionParameter parameter);         
    }
}
