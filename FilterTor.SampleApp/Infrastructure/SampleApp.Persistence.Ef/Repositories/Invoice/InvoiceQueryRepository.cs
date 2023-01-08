namespace SampleApp.Persistence.Ef.Repositories;

using FilterTor.Common.Entities;
using FilterTor.Decorators;
using Grid.Persistence;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InvoiceQueryRepository : EfGridQueryRepository<long, Invoice>, IInvoiceQueryRepository
{
    public InvoiceQueryRepository(SampleAppContext dbContext,
                                    ISortResolver<Invoice> sortResolver,
                                    IEntityResolver<Invoice> entityResolver) : base(dbContext, sortResolver, entityResolver)
    {
    }
}
