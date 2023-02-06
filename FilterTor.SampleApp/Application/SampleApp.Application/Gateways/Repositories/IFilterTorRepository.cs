namespace SampleApp.Application.Gateways; 

using FilterTor.Conditions;
using FilterTor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IFilterTorRepository<T>
{
    Task<List<T>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging);
}
