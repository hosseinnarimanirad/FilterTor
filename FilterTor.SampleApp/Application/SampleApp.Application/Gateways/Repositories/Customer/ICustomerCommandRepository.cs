namespace SampleApp.Application.Gateways.Repositories;
 
using SampleApp.Core.Entities; 
  
public interface ICustomerCommandRepository : ICommandRepository<long, Customer>
{
}
