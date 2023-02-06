namespace SampleApp.Persistence.Ef.Repositories;

using FilterTor.Resolvers;
using FilterTor.Strategies;
using Grid.Persistence;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InvoiceQueryRepository : EfFilterTorRepository<long, Invoice>, IInvoiceQueryRepository
{
    public InvoiceQueryRepository(SampleAppContext dbContext, FilterTorStrategy<Invoice> strategy) : base(dbContext, strategy)
    {
    }
}
