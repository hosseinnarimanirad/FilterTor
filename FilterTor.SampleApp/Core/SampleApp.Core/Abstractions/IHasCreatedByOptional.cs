namespace SampleApp.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHasCreatedByOptional
{
    string CreatedByFullName { get; }

    int? CreatedById { get; }
}
