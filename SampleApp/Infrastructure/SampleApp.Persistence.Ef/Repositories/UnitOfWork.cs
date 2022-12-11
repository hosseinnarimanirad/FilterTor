namespace SampleApp.Persistence.Ef.Repositories;

using Grid.Persistence;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Gateways;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly SampleAppContext _context;

    private bool _disposed;

    public IPolyFilterCommandRepository PolyFilters { get; private set; }

    public IInvoiceCommandRepository Invoices { get; private set; }

    public IInvoiceDetailCommandRepository InvoiceDetails { get; private set; }


    public UnitOfWork(SampleAppContext context)
    {
        _context = context;

        this.PolyFilters = new PolyFilterCommandRepository(context);

        this.Invoices = new InvoiceCommandRepository(context);

        this.InvoiceDetails = new InvoiceDetailCommandRepository(context);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}