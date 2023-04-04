namespace Grid.UnitTest;

using FilterTor;
using FilterTor.Conditions;
using FilterTor.Helpers;
using FilterTor.Targets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.FilterTorEx;
using SampleApp.FilterTorEx.Entities;

public class JsonCondition_Validation
{
    // **********************************************************************************
    //
    // **********************************************************************************
    [Fact]
    public void Condition_PropertyCondition_ConstantTarget_Deserialize_Method()
    {
        // ARRANGE
        string conditionJson = @"
        {
            ""category"":""property"",
            ""entity"":""invoice"",
            ""property"":""isSettled"",
            ""operation"":""equalsTo"",
            ""target"": {
                ""targetType"":""constant"",
                ""value"" : ""true"" 
                }
        }";

        // ACT
        var condition = JsonConditionBase.Deserialize(conditionJson) as JsonPropertyCondition;

        // ASSERT
        Assert.NotNull(condition);

        Assert.True(condition.Validate(new InvoiceResolver()));

        Assert.Equal(CategoryType.Property, condition.Category);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(condition.Entity, ignoreCase: true));

        Assert.Equal(InvoiceProperty.IsSettled, Enum.Parse<InvoiceProperty>(condition.Property, ignoreCase: true));
        Assert.Equal(Operation.EqualsTo, condition!.Operation);

        Assert.Equal(TargetType.Constant, condition!.Target?.TargetType);
        Assert.True(bool.Parse((condition!.Target as JsonConstantTarget)?.Value!));
    }


    // **********************************************************************************
    //
    // **********************************************************************************
    [Fact]
    public void Condition_PropertyCondition_RangeTarget_Deserialize_Method()
    {
        // ARRANGE
        string conditionJson = @"
        {
            ""category"":""property"",
            ""entity"":""invoice"",
            ""property"":""invoiceDate"",
            ""operation"":""between"",
            ""target"": {
                ""targetType"":""range"",
                ""minValue"" : ""1/1/2021"",
                ""maxValue"" : ""1/5/2022"" 
                }
        }";

        // ACT
        var condition = JsonConditionBase.Deserialize(conditionJson) as JsonPropertyCondition;

        // ASSERT
        Assert.NotNull(condition);

        Assert.True(condition.Validate(new InvoiceResolver()));

        Assert.Equal(CategoryType.Property, condition.Category);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(condition.Entity, ignoreCase: true));
        Assert.Equal(InvoiceProperty.InvoiceDate, Enum.Parse<InvoiceProperty>(condition.Property, ignoreCase: true));

        Assert.Equal(Operation.Between, condition!.Operation);

        Assert.Equal(TargetType.Range, condition!.Target?.TargetType);
        Assert.Equal(new DateTime(2021, 1, 1), DateTime.Parse((condition.Target as JsonRangeTarget)!.MinValue));
        Assert.Equal(new DateTime(2022, 1, 5), DateTime.Parse((condition.Target as JsonRangeTarget)!.MaxValue));
    }


    // **********************************************************************************
    //
    // **********************************************************************************
    [Fact]
    public void Condition_PropertyCondition_ArrayTarget_Deserialize_Method()
    {
        // ARRANGE
        string conditionJson = @"
        {
            ""category"":""property"",
            ""entity"":""invoice"",
            ""property"":""invoiceNumber"",
            ""operation"":""in"",
            ""target"": {
                ""targetType"":""array"",
                ""values"" : [""100100"",""100101"", ""100104""]
                }
        }";

        // ACT
        var condition = JsonConditionBase.Deserialize(conditionJson) as JsonPropertyCondition;

        // ASSERT
        Assert.NotNull(condition);

        Assert.True(condition.Validate(new InvoiceResolver()));

        Assert.Equal(CategoryType.Property, condition.Category);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(condition.Entity, ignoreCase: true));
        Assert.Equal(InvoiceProperty.InvoiceNumber, Enum.Parse<InvoiceProperty>(condition.Property, ignoreCase: true));

        Assert.Equal(Operation.In, condition!.Operation);

        Assert.Equal(TargetType.Array, condition!.Target?.TargetType);
        Assert.Equal(new List<string>() { "100100", "100101", "100104" }, (condition.Target as JsonArrayTarget)!.Values);
    }


    // **********************************************************************************
    //
    // **********************************************************************************
    [Fact]
    public void Condition_PropertyCondition_PropertyTarget_Deserialize_Method()
    {
        // ARRANGE
        string conditionJson = @"
        {
            ""category"":""property"",
            ""entity"":""invoice"",
            ""property"":""totalAmount"",
            ""operation"":""equalsTo"",
            ""target"": {
                ""targetType"":""property"",
                ""entity"":""invoice"",
                ""property"":""totalAmount""
                }
        }";

        // ACT
        var condition = JsonConditionBase.Deserialize(conditionJson) as JsonPropertyCondition;

        // ASSERT
        Assert.NotNull(condition);

        var target = condition.Target as JsonPropertyTarget;

        Assert.NotNull(target);

        Assert.True(condition.Validate(new InvoiceResolver()));

        Assert.Equal(CategoryType.Property, condition.Category);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(condition.Entity, ignoreCase: true));
        Assert.Equal(InvoiceProperty.TotalAmount, Enum.Parse<InvoiceProperty>(condition.Property, ignoreCase: true));

        Assert.Equal(Operation.EqualsTo, condition!.Operation);

        Assert.Equal(TargetType.Property, condition!.Target?.TargetType);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(target.Entity, ignoreCase: true));
        Assert.Equal(InvoiceProperty.TotalAmount, Enum.Parse<InvoiceProperty>(target!.Property, ignoreCase: true));
    }


    // **********************************************************************************
    //
    // **********************************************************************************
    [Fact]
    public void Condition_PropertyCondition_CollectionPropertyTarget_Deserialize_Method()
    {
        // ARRANGE
        string conditionJson = @"
        {
            ""category"":""property"",
            ""entity"":""invoice"",
            ""property"":""customerId"",
            ""operation"":""excludeAll"",
            ""target"": {
                ""targetType"":""collectionProperty"",
                ""entity"":""invoice"",
                ""collection"":""productIds""
                }
        }";

        // ACT
        var condition = JsonConditionBase.Deserialize(conditionJson) as JsonPropertyCondition;

        // ASSERT
        Assert.NotNull(condition);

        var target = condition.Target as JsonCollectionPropertyTarget;

        Assert.NotNull(target);

        Assert.True(condition.Validate(new InvoiceResolver()));

        Assert.Equal(CategoryType.Property, condition.Category);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(condition.Entity, ignoreCase: true));
        Assert.Equal(InvoiceProperty.CustomerId, Enum.Parse<InvoiceProperty>(condition.Property, ignoreCase: true));        

        Assert.Equal(Operation.ExcludeAll, condition!.Operation);

        Assert.Equal(TargetType.CollectionProperty, condition!.Target?.TargetType);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(target!.Entity, ignoreCase: true));
        Assert.Equal(InvoiceCollectionProperty.ProductIds, Enum.Parse<InvoiceCollectionProperty>(target!.Collection, ignoreCase: true));
    }


    // **********************************************************************************
    //
    // **********************************************************************************
    [Fact]
    public void Condition_PropertyCondition_MeasureTarget_Deserialize_Method()
    {
        // ARRANGE
        string conditionJson = @"
        {
            ""category"":""property"",
            ""entity"":""invoice"",
            ""property"":""totalAmount"",
            ""operation"":""equalsTo"",
            ""target"": {
                ""targetType"":""measure"",
                ""entity"":""invoice"",
                ""measure"":""sumDetailInvoicesPrice""
                }
        }";

        // ACT
        var condition = JsonConditionBase.Deserialize(conditionJson) as JsonPropertyCondition;

        // ASSERT
        Assert.NotNull(condition);

        var target = condition.Target as JsonMeasureTarget;

        Assert.NotNull(target);

        Assert.True(condition.Validate(new InvoiceResolver()));

        Assert.Equal(CategoryType.Property, condition.Category);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(condition.Entity, ignoreCase: true));
        Assert.Equal(InvoiceProperty.TotalAmount, Enum.Parse<InvoiceProperty>(condition.Property, ignoreCase: true));

        Assert.Equal(Operation.EqualsTo, condition!.Operation);

        Assert.Equal(TargetType.Measure, condition!.Target?.TargetType);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(target!.Entity, ignoreCase: true));
        Assert.Equal(InvoiceMeasure.SumDetailInvoicesPrice, Enum.Parse<InvoiceMeasure>(target!.Measure, ignoreCase: true));
    }


    // **********************************************************************************
    //
    // **********************************************************************************
    [Fact]
    public void Condition_CollectionPropertyCondition_ConstantTarget_Method()
    {
        // ARRANGE
        string conditionJson = @"
        {
            ""category"":""collectionProperty"",
            ""entity"":""invoice"",
            ""collection"":""productIds"",
            ""durationType"":""none"",
            ""operation"":""excludeAll"",
            ""target"": {
                ""targetType"":""array"",
                ""values"" : [""100"",""101""]
                }
        }";

        // ACT
        var condition = JsonConditionBase.Deserialize(conditionJson) as JsonCollectionPropertyCondition;

        // ASSERT
        Assert.NotNull(condition);

        Assert.True(condition.Validate(new InvoiceResolver()));

        Assert.Equal(CategoryType.CollectionProperty, condition.Category);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(condition.Entity, ignoreCase: true));
        //Assert.Equal(InvoiceCollectionProperty.ProductIds, Enum.Parse<InvoiceCollectionProperty>(condition.Collection, ignoreCase: true));

        Assert.Equal(DurationType.None, condition.DurationType);
        Assert.Equal(Operation.ExcludeAll, condition!.Operation);

        Assert.Equal(TargetType.Array, condition!.Target?.TargetType);
        Assert.Equal(new List<string>() { "100", "101" }, (condition.Target as JsonArrayTarget)!.Values);
    }


    // **********************************************************************************
    //
    // **********************************************************************************
    [Fact]
    public void Condition_MeasureCondition_ConstantTarget_Method()
    {
        // ARRANGE
        string conditionJson = @"
        {
            ""category"":""measure"",
            ""entity"":""invoice"",
            ""measure"":""SumDetailInvoicesPrice"",
            ""durationType"":""none"",
            ""operation"":""greaterThan"",
            ""target"": {
                ""targetType"":""constant"",
                ""value"" : ""30000.0""
                }
        }";

        // ACT
        var condition = JsonConditionBase.Deserialize(conditionJson) as JsonMeasureCondition;

        // ASSERT
        Assert.NotNull(condition);

        Assert.True(condition.Validate(new InvoiceResolver()));

        Assert.Equal(CategoryType.Measure, condition.Category);
        Assert.Equal(EntityType.Invoice, Enum.Parse<EntityType>(condition.Entity, ignoreCase: true));
        Assert.Equal(InvoiceMeasure.SumDetailInvoicesPrice, Enum.Parse<InvoiceMeasure>(condition.Measure, ignoreCase: true));

        Assert.Equal(DurationType.None, condition.DurationType);
        Assert.Equal(Operation.GreaterThan, condition!.Operation);

        Assert.Equal(TargetType.Constant, condition!.Target?.TargetType);
        Assert.Equal("30000.0", (condition.Target as JsonConstantTarget)!.Value);
    }


    // **********************************************************************************
    //
    // **********************************************************************************
    // todo: condition test for list
}
