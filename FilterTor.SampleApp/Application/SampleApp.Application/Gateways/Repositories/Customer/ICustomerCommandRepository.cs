namespace SampleApp.Application.Gateways.Repositories;
 
using SampleApp.Core.Entities; 
 


public interface ICustomerCommandRepository : IEfCommandRepository<long, Customer>
{
}
