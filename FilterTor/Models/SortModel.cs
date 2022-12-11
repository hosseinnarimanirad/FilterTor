namespace FilterTor.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


public class SortModel
{
    public required string Entity { get; set; }

    public required string Property { get; set; }
    
    // 1401.09.16
    // sorting based on measure is not supported
    //public required string Measure { get; set; }

    public ListSortDirection SortDirection { get; set; }
     
    public SortModel(string entity, string property) : this(entity, property, ListSortDirection.Ascending)
    {
    }

    public SortModel(string entity, string property, ListSortDirection sortDirection)
    {
        Entity = entity;

        Property = property;

        SortDirection = sortDirection;
    }
}

//public class SortModel<T>
//{
//    public string Entity { get; set; }

//    public string Property { get; set; }

//    public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;

//    public SortModel(string entity, string property)
//    {
//        Entity = entity;

//        Property = property;
//    }

//    public SortModel(string entity, string property, ListSortDirection sortDirection)
//    {
//        Entity = entity;

//        Property = property;

//        SortDirection = sortDirection;
//    }
//}