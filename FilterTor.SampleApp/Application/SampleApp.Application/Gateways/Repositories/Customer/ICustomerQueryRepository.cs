namespace SampleApp.Application.Gateways.Repositories;

using SampleApp.Core.Entities;
 
public interface ICustomerQueryRepository : IQueryRepository<long, Customer>
{
}
