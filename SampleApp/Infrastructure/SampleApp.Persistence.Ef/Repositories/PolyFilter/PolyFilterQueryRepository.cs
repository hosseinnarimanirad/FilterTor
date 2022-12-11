namespace SampleApp.Persistence.Ef.Repositories;

using Grid.Persistence;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PolyFilterQueryRepository : EfQueryRepository<int, PolyFilter>, IPolyFilterQueryRepository
{
    public PolyFilterQueryRepository(SampleAppContext dbContext) : base(dbContext)
    {
    }
     
}
