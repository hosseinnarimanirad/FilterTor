namespace SampleApp.Persistence.Ef.Repositories;

using FilterTor.Resolvers;
using Grid.Persistence;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CustomerQueryRepository : EfGridQueryRepository<long, Customer>, ICustomerQueryRepository
{
    public CustomerQueryRepository(SampleAppContext dbContext,
                                    ISortResolver<Customer> sortResolver,
                                    IEntityResolver<Customer> entityResolver) : base(dbContext, sortResolver, entityResolver)
    {
    }
}
