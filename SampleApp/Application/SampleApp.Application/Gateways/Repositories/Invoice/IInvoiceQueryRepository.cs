namespace SampleApp.Application.Gateways.Repositories;

using SampleApp.Core.Entities;



public interface IInvoiceQueryRepository : IEfQueryRepository<long, Invoice>,
                                            IGridQueryRepository<long, Invoice>
{
}
