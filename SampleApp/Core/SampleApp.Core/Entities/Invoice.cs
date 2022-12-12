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
    // possibly encapsulation violation
    public required long Id { get; init; }

    public required DateTime CreateTime { get; init; }

    public required string InvoiceNumber { get; init; }

    public DateTime InvoiceDate { get; private set; }

    public bool IsSettled { get; private set; }

    public decimal TotalAmount { get; private set; }

    public long CustomerId { get; private set; }

    public InvoiceType InvoiceType { get; private set; }

    private readonly List<InvoiceDetail> _invoiceDetails = new List<InvoiceDetail>();
    public IEnumerable<InvoiceDetail> InvoiceDetails => _invoiceDetails.AsReadOnly();


    [SetsRequiredMembers]
    public Invoice(string invoiceNumber, DateTime invoiceDate, bool isSettled, decimal totalAmount, long customerId, InvoiceType invoiceType)
    {
        this.InvoiceNumber = invoiceNumber;
        this.CreateTime = DateTime.Now;

        this.InvoiceDate = invoiceDate;
        this.IsSettled = isSettled;
        this.TotalAmount = totalAmount;
        this.CustomerId = customerId;
        this.InvoiceType = invoiceType;
    }
}

