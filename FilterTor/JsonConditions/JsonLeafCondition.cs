namespace GridEngineCore.Conditions;
  
using GridEngineCore.Targets; 
using System.Text.Json.Serialization;


public class JsonLeafCondition : JsonConditionBase
{
    public required string Entity { get; set; }

    public JsonTargetBase? Target { get; set; }

    public Operation? Operation { get; set; }

    public override bool Validate() => base.Validate() && !string.IsNullOrWhiteSpace(Entity);

}
