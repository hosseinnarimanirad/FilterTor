namespace SampleApp.Application.Dto;

using FilterTor.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHasCustomerCondition
{
    JsonConditionBase CustomerCondition { get; }
}
