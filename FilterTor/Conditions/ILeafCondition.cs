namespace FilterTor;


// COMPOSITE DESIGN PATTERN: Leaf

public interface ILeafCondition : ICondition
{
    string Entity { get; }
}
