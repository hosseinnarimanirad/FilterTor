namespace SampleApp.Core.Entities;

using SampleApp.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomerGroup : IHasKey<long>, IHasCreateTimeRequired
{
    // possibly encapsulation violation
    public required long Id { get; init; }

    public required DateTime CreateTime { get; set; }

    public CustomerGroupType Type { get; set; }

    public long CustomerId { get; set; }

    public Customer Customer { get; set; }
}
