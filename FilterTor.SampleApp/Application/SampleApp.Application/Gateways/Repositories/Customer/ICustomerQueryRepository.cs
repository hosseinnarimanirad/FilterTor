namespace SampleApp.Application.Gateways.Repositories;

using SampleApp.Core.Entities;



public interface ICustomerQueryRepository : IEfQueryRepository<long, Customer>,
                                            IGridQueryRepository<long, Customer>
{
}
