namespace SampleApp.Application.Features;

using SampleApp.Application.Common; 
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterTor.Conditions;

public class GetPolyFilterQuery : IRequest<GetPolyFilterResponse>
{
    public int PolyFilterId { get; set; }
}

public class GetPolyFilterResponse : IHasCondition
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Note { get; set; }


    public string CreatedByFullName { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreateTime { get; set; }


    public bool IsFavorite { get; set; }

    public bool IsPublic { get; set; }

    public JsonConditionBase Condition { get; set; }

}

