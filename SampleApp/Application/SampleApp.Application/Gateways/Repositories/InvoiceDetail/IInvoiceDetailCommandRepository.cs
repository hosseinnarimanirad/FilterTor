namespace SampleApp.Application.Gateways.Repositories;

using SampleApp.Core.Entities;



public interface IInvoiceDetailCommandRepository : IEfCommandRepository<long, InvoiceDetail>
{
}
