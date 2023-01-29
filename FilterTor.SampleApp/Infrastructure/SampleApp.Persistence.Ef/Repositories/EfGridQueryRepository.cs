//namespace SampleApp.Persistence.Ef.Repositories;

//using FilterTor.Resolvers;
//using Grid.Persistence;
//using SampleApp.Core;

//public abstract class EfGridQueryRepository<TKey, TEntity> : EfQueryRepository<TKey, TEntity>
//                                                                where TEntity : class, IHasKey<TKey>
//                                                                where TKey : struct
//{
//    protected EfGridQueryRepository(SampleAppContext dbContext,
//                                    ISortResolver<TEntity> sortResolver,
//                                    IEntityResolver<TEntity> entityResolver) : base(dbContext, sortResolver, entityResolver)
//    { 
//    }
     
//    //public async Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
//    //{
//    //    IQueryGenerator<TEntity> result = this;

//    //    if (jsonCondition is not null)
//    //    {
//    //        result = new FilterDecorator<TEntity>(result, jsonCondition, _entityResolver);
//    //    }

//    //    if (!sorts.IsNullOrEmpty())
//    //    {
//    //        result = new SortDecorator<TEntity>(result, sorts!, _sortResolver);
//    //    }

//    //    if (paging is not null)
//    //    {
//    //        result = new PagingDecorator<TEntity>(result, paging);
//    //    }

//    //    return await result.Query(this._context.Set<TEntity>().AsQueryable()).ToListAsync();
//    //}
//}