namespace SampleApp.Persistence.Ef.Repositories;

using Grid.Persistence;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InvoiceDetailCommandRepository : EfCommandRepository<long, InvoiceDetail>, IInvoiceDetailCommandRepository
{
    public InvoiceDetailCommandRepository(GridContext dbContext) : base(dbContext)
    {
    }
}