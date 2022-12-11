﻿namespace SampleApp.Persistence.Ef.Repositories;

using Grid.Persistence;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InvoiceDetailQueryRepository : EfGridQueryRepository<long, InvoiceDetail>, IInvoiceDetailQueryRepository
{
    public InvoiceDetailQueryRepository(SampleAppContext dbContext) : base(dbContext)
    {
    }
}
