namespace SampleApp.Core.Entities;

using SampleApp.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Invoice : IHasKey<long>, IHasCreateTimeRequired
{
    public long Id { get; private set; }

    public required DateTime CreateTime { get; init; }

    public required string InvoiceNumber { get; init; }

    public DateTime InvoiceDate { get; private set; }

    public bool IsSettled { get; private set; }

    public decimal TotalAmount { get; private set; }

    public decimal Discount { get; private set; }

    public long CustomerId { get; private set; }

    public InvoiceType InvoiceType { get; private set; }

    public ICollection<InvoiceDetail> InvoiceDetails { get; set; }


    //public Invoice(string invoiceNumber)
    //{
    //    this.InvoiceNumber = invoiceNumber;
    //    this.CreateTime = DateTime.Now;
    //}

    [SetsRequiredMembers]
    public Invoice(long id, string invoiceNumber, DateTime invoiceDate, bool isSettled, decimal totalAmount, decimal discount, long customerId, InvoiceType invoiceType)
    {
        this.InvoiceNumber = invoiceNumber;
        this.CreateTime = DateTime.Now;

        this.Id = id;
        this.InvoiceDate = invoiceDate;
        this.IsSettled = isSettled;
        this.TotalAmount = totalAmount;
        this.Discount = discount;
        this.CustomerId = customerId;
        this.InvoiceType = invoiceType;
        this.InvoiceDetails = new List<InvoiceDetail>();
    }
}

