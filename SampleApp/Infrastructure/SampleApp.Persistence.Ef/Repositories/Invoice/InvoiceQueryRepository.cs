namespace SampleApp.Persistence.Ef.Repositories;


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
    public InvoiceQueryRepository(SampleAppContext dbContext) : base(dbContext)
    {
    }
}
