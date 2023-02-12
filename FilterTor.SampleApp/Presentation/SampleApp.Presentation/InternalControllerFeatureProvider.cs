using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Presentation; 

internal class InternalControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        if (!typeInfo.IsClass)
        {
            return false;
        }
        if (typeInfo.IsAbstract)
        {
            return false;
        }
        if (typeInfo.ContainsGenericParameters)
        {
            return false;
        }
        if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
        {
            return false;
        }
        return typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) ||
                typeInfo.IsDefined(typeof(ControllerAttribute));
    }
}