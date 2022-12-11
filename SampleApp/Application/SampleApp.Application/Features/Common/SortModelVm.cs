namespace SampleApp.Application.Features;

using FilterTor.Common.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


public class SortModelVm
{
    public required string Property { get; set; }

    [JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
    public ListSortDirection SortDirection { get; set; } = ListSortDirection.Descending;
}