namespace SampleApp.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CustomerGroupType
{
    [Description("بخش خصوصی")]
    PrivateSector = 1,

    [Description("بخش دولتی")]
    Government = 2,

    [Description("محدود")]
    Limited = 3,

    [Description("معلق")]
    Suspended = 4,

    [Description("مشتری خاص")]
    Golden = 5

}
