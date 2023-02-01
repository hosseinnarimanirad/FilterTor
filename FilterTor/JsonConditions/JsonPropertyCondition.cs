namespace FilterTor.Conditions;

using FilterTor.Helpers;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;


public class JsonPropertyCondition : JsonLeafCondition
{ 
    public required string Property { get; set; }
    
    public JsonPropertyCondition()
    {
        Category = CategoryType.Property;
    }

    // 1399.10.27
    public override bool Validate()
    {
        return base.Validate()
                && Category == CategoryType.Property
                // 1399.11.28
                // در هر صورت این مورد برای شرط
                // انتیتی لازم است که مشخص شده باشد
                && HasValidOperation()

                // 1399.12.19
                // برای شرط انتیتی در هر صورت مقدار
                // تارگت هم باید چک بشه
                && HasValidTarget();
    }

    public bool HasValidOperation()
    {
        return Operation != 0;
    }

    public bool HasValidTarget()
    {
        return Target is not null
                && Target.Validate();
    }

    public override string ToString()
    {
        return $"{Operation.GetDescription()} {Target}";
    }

    public override List<string> GetSubConditions()
    {
        var conditions = new List<string>
        {
            Property 
        }; 

        return conditions.ToList();
    }
}
