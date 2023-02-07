namespace SampleApp.Persistence.Ef.Repositories;

using FilterTor.Conditions;
using FilterTor.Models;
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


public class CustomerQueryRepository : EfFilterTorRepository<long, Customer>, ICustomerQueryRepository
{
    public CustomerQueryRepository(SampleAppContext dbContext, FilterTorStrategyContext<Customer> strategy) : base(dbContext, strategy)
    {
    }
     
}
