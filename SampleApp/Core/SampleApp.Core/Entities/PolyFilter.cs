namespace SampleApp.Core.Entities;

using FilterTor.Conditions;
using SampleApp.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PolyFilter : IHasKey<int>, IHasCreatedByRequired, IHasCreateTimeRequired
{
    public int Id { get; set; }

    public string? Title { get; private set; }

    public string? Note { get; private set; }


    public string CreatedByFullName { get; private set; }

    public int? CreatedById { get; private set; }

    public DateTime? CreateTime { get; private set; }


    public bool IsFavorite { get; private set; }

    public bool IsPublic { get; private set; }

    public string ConditionJson { get; private set; }
     

    public PolyFilter()
    {
        this.Title = String.Empty;
        this.CreatedByFullName = String.Empty;
        this.ConditionJson = String.Empty;
    }

    public void Update(string title, string note)
    {
        this.Title = title;
        this.Note = note;
    }

    public static PolyFilter Create(
        JsonConditionBase condition,
        string createdByFullName,
        int? createdById,
        bool isFavorite,
        bool isPublic,
        string note,
        string title
        )
    {
        if (condition is null || !condition.Validate())
            throw new NotImplementedException("invalid condition");

        return new PolyFilter()
        {
            ConditionJson = condition.Serialize(null),
            CreatedByFullName = createdByFullName,
            CreatedById = createdById,
            IsFavorite = isFavorite,
            IsPublic = isPublic,
            Note = note,
            Title = title
        };
    }

    public void ToggleIsFavorite()
    {
        this.IsFavorite = !this.IsFavorite;
    }

    public void ToggleIsPublic()
    {
        this.IsPublic = !this.IsPublic;
    }
}
