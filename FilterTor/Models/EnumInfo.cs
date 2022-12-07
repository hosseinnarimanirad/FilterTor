namespace GridEngineCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public record EnumInfo(int Id, string? PropertyNameEn, string? PropertyNameFa)
{
    public EnumInfo() : this(default, string.Empty, string.Empty)
    {

    }

}
