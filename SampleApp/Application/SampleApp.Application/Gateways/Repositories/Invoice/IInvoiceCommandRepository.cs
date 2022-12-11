namespace SampleApp.Application.Gateways.Repositories;
 
using SampleApp.Core.Entities; 
 


public interface IInvoiceCommandRepository : IEfCommandRepository<long, Invoice>
{
}
