namespace SampleApp.Application.Gateways.Repositories;

using SampleApp.Core.Entities;
 
public interface IInvoiceQueryRepository : IQueryRepository<long, Invoice>, IFilterTorRepository<Invoice>
{
}
