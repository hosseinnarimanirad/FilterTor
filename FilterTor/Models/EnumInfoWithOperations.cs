namespace FilterTor.Models;

using System;
using System.Collections.Generic;
using System.Text;


public record EnumInfoWithOperations : EnumInfo
{
    public OperationControlType ControlType { get; set; }
    //public string ControlType { get; set; }

    //public List<LogicalOperations> Operations { get; set; }
    //public List<string> Operations { get; set; }

    //public string GetAllPossibleValueFunc { get; set; }
}
