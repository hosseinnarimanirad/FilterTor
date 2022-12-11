namespace SampleApp.Application.Features;

using SampleApp.Application.Common; 
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterTor.Conditions;

public class AddPolyFilterCommand : IRequest<AddPolyFilterResponse>, IHasCondition
{
    public string Title { get;   set; }

    public string? Note { get;   set; }


    public string CreatedByFullName { get;   set; }

    public int CreatedById { get;   set; }

    public bool IsFavorite { get;   set; }

    public bool IsPublic { get;   set; }

    public JsonConditionBase Condition { get; set; }

}

public class AddPolyFilterResponse  
{
    public bool IsSucceed { get; set; } = false;
}

