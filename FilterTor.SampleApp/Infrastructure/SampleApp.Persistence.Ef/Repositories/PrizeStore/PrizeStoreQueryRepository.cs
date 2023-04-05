namespace SampleApp.Persistence.Ef.Repositories;

using SampleApp.Persistence;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PrizeStoreQueryRepository : EfQueryRepository<int, PrizeStore>, IPrizeStoreQueryRepository
{
    public PrizeStoreQueryRepository(SampleAppContext dbContext) : base(dbContext)
    {
    }

}
