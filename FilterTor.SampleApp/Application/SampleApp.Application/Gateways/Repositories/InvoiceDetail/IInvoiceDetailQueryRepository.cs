namespace SampleApp.Application.Gateways.Repositories;

using SampleApp.Core.Entities;
 
public interface IInvoiceDetailQueryRepository : IQueryRepository<long, InvoiceDetail>
{
}
