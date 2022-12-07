namespace GridEngineCore.Models;

using GridEngineCore.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class ListModel
{ 
    public required string GridKey { get; set; }

    public PagingModel? Paging { get; set; }
     
    public List<SortModel>? Sorts { get; set; }

    public JsonConditionBase? InlineFilter { get; set; }

    public JsonConditionBase? PolyFilter { get; set; }
}
