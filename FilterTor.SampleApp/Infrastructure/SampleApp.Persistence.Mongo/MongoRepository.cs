
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using SampleApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Api.Infrastructure.MongoRepositories
{
    public class MongoRepository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TEntity : class, IHasKey<TKey>
    {
        protected readonly MongoContext _context;

        protected readonly IMongoCollection<TEntity> DbSet;

        private static readonly string _keyField = "_id";


        private FilterDefinition<TEntity> FilterById(TKey id)
        {
            return Builders<TEntity>.Filter.Eq(new StringFieldDefinition<TEntity, TKey>(_keyField), id);
        }

        private FilterDefinition<TEntity> NoFilter()
        {
            return Builders<TEntity>.Filter.Empty;
        }

        public MongoRepository()
        {

        }

        public MongoRepository(MongoContext context)
        {
            _context = context;

            this.DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);

            //1399.07.05
            //هنگام کار با مانگو از طریق زبان‌های مختلف ممکن است
            //به شکل‌های مختلفی جی‌یو‌ای‌دی تولید شود برای جلوگیری 
            //بروز مشکل هنگام اپدیت این کد استفاده می‌شود
            //https://www.codeproject.com/Articles/987203/Best-Practices-for-GUID-data-in-MongoDB
            //BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            //https://stackoverflow.com/questions/63264362/mongocollectionsettings-guidrepresentation-is-obsolete-whats-the-alternative
            //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));


            //1399.07.05
            //مقادیر دسیمال به استرینگ تبدیل می‌شدن در سمت
            //مانگو هنگام ذخیره‌سازی
        }


        public IHasKey<TKey> Add(TEntity entity)
        {           
            _context.AddCommand(() => DbSet.InsertOne(entity));

            return entity;
        }

        public Task<IHasKey<TKey>> AddAsync(TDomain domain)
        {
            return Task.Run<IHasKey<TKey>>(() =>
            {
                var entity = _inverseMap(domain);

                _context.AddAsyncCommand(async () => await DbSet.InsertOneAsync(entity));

                return entity;
            });

        }

        public IEnumerable<IHasKey<TKey>> AddRange(IEnumerable<TDomain> domains)
        {
            if (domains == null || domains.Count() == 0)
            {
                return new List<TEntity>(); ;
            }

            var entities = domains.Select(d => _inverseMap(d));

            _context.AddCommand(() => DbSet.InsertMany(entities));

            return entities;
        }

        public Task<IEnumerable<IHasKey<TKey>>> AddRangeAsync(IEnumerable<TDomain> domains)
        {
            return Task.Run<IEnumerable<IHasKey<TKey>>>(() =>
            {
                if (domains == null || domains.Count() == 0)
                {
                    return new List<TEntity>(); ;
                }

                var entities = domains.Select(d => _inverseMap(d));

                _context.AddAsyncCommand(async () => await DbSet.InsertManyAsync(entities));

                return entities;
            });
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public TDomain Get(TKey id, bool withNoTrack = true)
        {
            ////Guid guidKey = Guid.Parse("38366e1a-fc06-4cd0-8ceb-a5038bd2d1b8");

            ////var data1 = DbSet.Find(Builders<TEntity>.Filter.Eq(_keyField, id)).ToList();
            ////var data2 = DbSet.Find(Builders<TEntity>.Filter.Eq(_keyField, id.ToString())).ToList();
            ////var data3 = DbSet.Find(Builders<TEntity>.Filter.Eq(new StringFieldDefinition<TEntity, TKey>(_keyField), id)).ToList();

            //////Builders<Model.SettlementMongoModel>.Filter.Eq("SettlementMongoModel._id", )

            ////var settlementDbSet = _context.GetCollection<Model.SettlementMongoModel>("SettlementMongoModel");

            ////var filter1 = Builders<Model.SettlementMongoModel>.Filter.Eq(new StringFieldDefinition<Model.SettlementMongoModel, long>("CustomerId"), 42510012);
            ////var find1 = settlementDbSet.Find<Model.SettlementMongoModel>(filter1).ToList();

            ////var filter2 = Builders<Model.SettlementMongoModel>.Filter.Eq(new StringFieldDefinition<Model.SettlementMongoModel, Guid>("_id"), guidKey);
            ////var find2 = settlementDbSet.Find<Model.SettlementMongoModel>(filter2).ToList();

            ////var filter3 = Builders<Model.SettlementMongoModel>.Filter.Eq(s => s.Id, guidKey);
            ////var find3 = settlementDbSet.Find<Model.SettlementMongoModel>(filter3).ToList();

            ////var filter4 = Builders<Model.SettlementMongoModel>.Filter.Eq("_id", guidKey);
            ////var find4 = settlementDbSet.Find<Model.SettlementMongoModel>(filter4).ToList();

            ////var filter5 = Builders<Model.SettlementMongoModel>.Filter.Eq("SettlementMongoModel._id", guidKey);
            ////var find5 = settlementDbSet.Find<Model.SettlementMongoModel>(filter5).ToList();

            ////var filter6 = Builders<Model.SettlementMongoModel>.Filter.Eq(new StringFieldDefinition<Model.SettlementMongoModel, Guid>("Id"), guidKey);
            ////var find6 = settlementDbSet.Find<Model.SettlementMongoModel>(filter6).ToList();

            //////var find = DbSet.Find(f => f.Id.Equals(id)).ToList();

            var data = DbSet.Find(FilterById(id));
            //var data4 = DbSet.Database.GetCollection<Model.SettlementMongoModel>("SettlementMongoModel").Find(_ => true).ToList();
            //var data5 = DbSet.Database.GetCollection<Model.SettlementMongoModel>("SettlementMongoModel").Find(Builders<Model.SettlementMongoModel>.Filter.Empty).ToList();
            return _map(data.FirstOrDefault());
        }

        public async Task<TDomain> GetAsync(TKey id, bool withNoTrack = true)
        {
            //var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(_keyField, id.ToString()));
            //var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(new StringFieldDefinition<TEntity, TKey>(_keyField), id));
            var data = await DbSet.FindAsync(FilterById(id));

            return _map(data.FirstOrDefault());
        }

        public async Task<TDomain> GetRootAsync(TKey id, bool withNoTrack = true)
        {
            var data = await DbSet.FindAsync(FilterById(id));

            return _map(data.FirstOrDefault());
        }

        public IEnumerable<TDomain> GetAll(bool withNoTrack = true)
        {
            //var all = DbSet.Find(Builders<TEntity>.Filter.Empty);
            var all = DbSet.Find(NoFilter());

            return all.ToList().Select(a => _map(a)).ToList();
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync(bool withNoTrack = true)
        {
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            var all = await DbSet.FindAsync(NoFilter());

            return all.ToList().Select(a => _map(a)).ToList();
        }

        public void Remove(TKey id)
        {
            //_context.AddCommand(() => DbSet.DeleteOne(Builders<TEntity>.Filter.Eq(_keyField, id.ToString())));
            //_context.AddCommand(() => DbSet.DeleteOne(Builders<TEntity>.Filter.Eq(new StringFieldDefinition<TEntity, TKey>(_keyField), id)));
            _context.AddCommand(() => DbSet.DeleteOne(FilterById(id)));
        }

        public Task RemoveAsync(TKey id)
        {
            return Task.Run(() =>
            {
                //_context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq(_keyField, id.ToString())));
                //_context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq(new StringFieldDefinition<TEntity, TKey>(_keyField), id)));
                _context.AddCommand(() => DbSet.DeleteOneAsync(FilterById(id)));
            });
        }


        public void Remove(TDomain domain)
        {
            if (domain == null)
            {
                return;
            }

            Remove(domain.Id);
        }

        public async Task RemoveAsync(TDomain domain)
        {
            if (domain == null)
            {
                return;
            }

            await RemoveAsync(domain.Id);
        }


        public void RemoveRange(IEnumerable<TDomain> domains)
        {
            if (domains == null)
            {
                return;
            }

            foreach (var item in domains)
            {
                Remove(item);
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<TDomain> domains)
        {
            if (domains == null)
            {
                return;
            }

            foreach (var item in domains)
            {
                await RemoveAsync(item);
            }
        }


        public void RemoveRange(IEnumerable<TKey> domainIds)
        {
            if (domainIds == null || domainIds.Any() != true)
            {
                return;
            }

            foreach (var domainId in domainIds)
            {
                Remove(domainId);
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<TKey> domainIds)
        {
            if (domainIds == null || domainIds.Any() != true)
            {
                return;
            }

            foreach (var domain in domainIds)
            {
                await RemoveAsync(domain);
            }
        }


        //todo: consider not updating the whole document
        public IHasKey<TKey> Update(TDomain domain)
        {
            var entity = _inverseMap(domain);

            _context.AddCommand(() =>
            {
                //DbSet.ReplaceOne(Builders<TEntity>.Filter.Eq(_keyField, domain.Id.ToString()), entity);
                //DbSet.ReplaceOne(Builders<TEntity>.Filter.Eq(new StringFieldDefinition<TEntity, TKey>(_keyField), domain.Id), entity);
                DbSet.ReplaceOne(FilterById(domain.Id), entity);

                //Filter.Eq(new StringFieldDefinition<TEntity, TKey>(_keyField), id)));
            });

            return entity;
        }

        //todo: consider not updating the whole document
        public Task<IHasKey<TKey>> UpdateAsync(TDomain domain)
        {
            return Task.Run<IHasKey<TKey>>(() =>
            {
                var entity = _inverseMap(domain);

                _context.AddCommand(async () =>
                {
                    //await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(_keyField, domain.Id), entity);
                    //await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(new StringFieldDefinition<TEntity, TKey>(_keyField), domain.Id), entity);
                    await DbSet.ReplaceOneAsync(FilterById(domain.Id), entity);
                });

                return entity;
            });
        }

        public void Dispose()
        {
            _context.Dispose();

            GC.SuppressFinalize(this);
        }


    }
}
