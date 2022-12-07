namespace GridEngineCore;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


//switch codes:
//1- Step.cs > GetStepRangeType
public enum RangeType
{
    [Description("تعداد")]
    Tedad = 1,

    [Description("قیمت")]
    Geymat = 2,

    [Description("درصد")]
    Darsad = 3,

    [Description("زمان")]
    Zaman = 4,
}