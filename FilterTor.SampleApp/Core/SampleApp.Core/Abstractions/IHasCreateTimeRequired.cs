namespace SampleApp.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHasCreateTimeRequired
{
    // IsRequired is handled in configure methods
    DateTime CreateTime { get;  }
}
