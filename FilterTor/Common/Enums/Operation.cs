namespace FilterTor;

using FilterTor.Attributes;
using FilterTor.Common.Converters;
using System.ComponentModel;
using System.Text.Json.Serialization;

/// <summary>
/// Defines the operations supported by the <seealso cref="Builders.FilterBuilder" />.
/// </summary>
[JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
public enum Operation
{
    /// <summary>
    /// Targets an object in which the property's value is equal to the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("مساوی")]
    EqualTo = 1,

    /// <summary>
    /// Targets an object in which the property's value contains part of the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("شامل")]
    Contains = 2,

    /// <summary>
    /// Targets an object in which the property's value starts with the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("شروع شود با")]
    StartsWith = 3,

    /// <summary>
    /// Targets an object in which the property's value ends with the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("تمام شود با")]
    EndsWith = 4,

    /// <summary>
    /// Targets an object in which the property's value is not equal to the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("نامساوی با")]
    NotEqualTo = 5,

    /// <summary>
    /// Targets an object in which the property's value is greater than the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("بزرگ‌تر از")]
    GreaterThan = 6,

    /// <summary>
    /// Targets an object in which the property's value is greater than or equal to the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("بزرگ مساوی")]
    GreaterThanOrEqualTo = 7,

    /// <summary>
    /// Targets an object in which the property's value is less than the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("کوچک‌تر از")]
    LessThan = 8,

    /// <summary>
    /// Targets an object in which the property's value is less than or equal to the provided value.
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("کوچک‌تر مساوی")]
    LessThanOrEqualTo = 9,

    /// <summary>
    /// Targets an object in which the property's value is between the two provided values (greater than or equal to the first one and less then or equal to the second one).
    /// </summary>
    /// <remarks>Accepts two values.</remarks>
    [NumberOfValues(2), Description("بین")]
    Between = 10,

    ///// <summary>
    ///// Targets an object in which the property's value is null.
    ///// </summary>
    ///// <remarks>Accepts no value at all.</remarks>
    //[NumberOfValues(0), Description("بدون مقدار")]
    //IsNull = 11,

    ///// <summary>
    ///// Targets an object in which the property's value is an empty string.
    ///// </summary>
    ///// <remarks>Accepts no value at all.</remarks>
    //[NumberOfValues(0), Description("خالی")]
    //IsEmpty = 12,

    ///// <summary>
    ///// Targets an object in which the property's value is null or an empty string.
    ///// </summary>
    ///// <remarks>Accepts no value at all.</remarks>
    //[NumberOfValues(0), Description("بدون مقدار یا خالی")]
    //IsNullOrWhiteSpace = 13,

    ///// <summary>
    ///// Targets an object in which the property's value is not null.
    ///// </summary>
    ///// <remarks>Accepts no value at all.</remarks>
    //[NumberOfValues(0), Description("دارای مقدار")]
    //IsNotNull = 14,

    ///// <summary>
    ///// Targets an object in which the property's value is not an empty string.
    ///// </summary>
    ///// <remarks>Accepts no value at all.</remarks>
    //[NumberOfValues(0), Description("ناخالی")]
    //IsNotEmpty = 15,

    ///// <summary>
    ///// Targets an object in which the property's value is neither null nor an empty string.
    ///// </summary>
    ///// <remarks>Accepts no value at all.</remarks>
    //[NumberOfValues(0), Description("دارای مقدار")]
    //IsNotNullNorWhiteSpace = 16,

    /// <summary>
    /// Targets an object in which the provided value is presented in the property's value (as a list).
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("مشمول")]
    In = 17,

    /// <summary>
    /// Targets an object in which the provided value is presented in the property's value (as a list).
    /// </summary>
    /// <remarks>Accepts one value.</remarks>
    [NumberOfValues(1), Description("نا مشمول")]
    NotIn = 18,


    [NumberOfValues(1), Description("مشمول")] // شامل برخی
    IncludeAny = 20,       //: Intersects

    [NumberOfValues(1), Description("مشمول همه")] // شامل همه
    IncludeAll = 21,       //: contains

    [NumberOfValues(1), Description("نامشمول")] // به جز برخی
    ExcludeAny = 22,    //: not Intersects

    [NumberOfValues(1), Description("نامشمول همه")] // به جز همه
    ExcludeAll = 23,     //: notContains
}


public enum OperationControlType
{
    SingleEnum = 1,
    MultiEnum = 2,
    Numeric = 3,
    RangeNumeric = 4,
    AutoComplete = 5,
    DateTime = 6,
    Checkbox = 7,
    Textbox = 8,

    MultiEnumForArray = 9,
    SingleEnumForArray = 10
}
