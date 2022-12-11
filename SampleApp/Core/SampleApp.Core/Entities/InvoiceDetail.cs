namespace SampleApp.Core.Entities;

using SampleApp.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvoiceDetail : IHasKey<long>
{
    public long Id { get; private set; }

    public long InvoiceId { get; private set; }

    public Invoice Invoice { get; private set; }

    public long ProductId { get; private set; }

    public int Count { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal Discount { get; private set; }

    public bool IsPrize { get; private set; }

    public InvoiceDetail(long id, long invoiceId, long productId, int count, decimal unitPrice, decimal discount, bool isPrize)
    {
        this.Id = id;
        this.InvoiceId = invoiceId;
        this.ProductId = productId;
        this.Count = count;
        this.UnitPrice = unitPrice;
        this.Discount = discount;
        this.IsPrize = isPrize;
    }
}
