namespace SampleApp.Application.Gateways.Repositories;

using SampleApp.Core.Entities;



public interface IInvoiceDetailQueryRepository : IEfQueryRepository<long, InvoiceDetail>,
                                                    IGridQueryRepository<long, InvoiceDetail>
{
}
