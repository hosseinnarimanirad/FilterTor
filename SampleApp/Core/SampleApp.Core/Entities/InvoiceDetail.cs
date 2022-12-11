namespace SampleApp.Core.Entities;

using SampleApp.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvoiceDetail : IHasKey<long>
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public Invoice Invoice { get; set; }

    public long ProductId { get; set; }

    public int Count { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public bool IsPrize { get; set; }
}
