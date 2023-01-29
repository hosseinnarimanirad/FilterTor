namespace SampleApp.Application.Gateways.Repositories;

using SampleApp.Core.Entities;
  
public interface IPrizeStoreCommandRepository : ICommandRepository<int, PrizeStore>
{
}
