using FilterTor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FilterTor.Json
{
    // 1401.05.12
    // https://www.michaelrose.dev/posts/exploring-system-text-json/
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();         
        }

    }
}
