namespace SampleApp.Application.Gateways;

using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core;
using SampleApp.Core.Entities;
using System.Linq.Expressions;

public interface IUnitOfWork : IDisposable
{
    IPrizeStoreCommandRepository PrizeStores { get; }

    IInvoiceCommandRepository Invoices { get; }

    IInvoiceDetailCommandRepository InvoiceDetails { get; }


    void SaveChanges();

    Task SaveChangesAsync();

}