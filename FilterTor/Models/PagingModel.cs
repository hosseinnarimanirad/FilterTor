namespace FilterTor.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PagingModel
{
    public required int Page { get; set; }

    public required int PageSize { get; set; }
}
