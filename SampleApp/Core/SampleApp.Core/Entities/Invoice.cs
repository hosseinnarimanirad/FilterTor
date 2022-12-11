namespace SampleApp.Core.Entities;

using SampleApp.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Invoice : IHasKey<long>
{
    public long Id { get; set; }

    public required string InvoiceNumber { get; init; }

    public DateTime InvoiceDate { get; private set; }

    public bool IsSettled { get; private set; }

    public decimal TotalAmount { get; private set; }

    public decimal Discount { get; private set; }

    public long CustomerId { get; private set; }

    public InvoiceType InvoiceType { get; private set; }

    public virtual ICollection<InvoiceDetail>? InvoiceDetails { get; set; }
     
}
 
