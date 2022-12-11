namespace FilterTor.Models;

using FilterTor.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class ListModel
{ 
    // entity type
    public required string Entity { get; set; }

    public PagingModel? Paging { get; set; }
     
    public List<SortModel>? Sorts { get; set; }

    public JsonConditionBase? InlineFilter { get; set; }

    public JsonConditionBase? PolyFilter { get; set; }
}
