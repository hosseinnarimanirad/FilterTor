namespace SampleApp.FilterTorEx.Entities;

using System.ComponentModel;



public enum CustomerProperty
{
    [Description("تاریخ ثبت")]
    RegisteredDate = 1,

    [Description("اعتبار")]
    Credit = 2,
}
 