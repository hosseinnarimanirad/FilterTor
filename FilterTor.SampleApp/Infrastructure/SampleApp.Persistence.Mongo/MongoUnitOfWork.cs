//using AutoMapper;
//using CommonInfrastructure.Persistence;
//using Finance.Api.Core.Interfaces.Repositories;
//using Sieve.Services;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Finance.Api.Infrastructure.MongoRepositories
//{
//    public class MongoUnitOfWork : IMongoUnitOfWork
//    {
//        private readonly MongoContext _context;

//        private IMapper _mapper;

//        private SieveProcessor _sieveProcessor;

//        public MongoUnitOfWork(MongoContext context, IMapper mapper, SieveProcessor sieveProcessor)
//        {
//            _context = context;

//            _mapper = mapper;

//            _sieveProcessor = sieveProcessor;

//            SettlementMongoRepository = new SettlementMongoRepository(context, mapper);

//            //ReceiptRepository = new ReceiptMongoRepository(context, mapper);
//        }

//        //public ITempReceiptPaymentRepository TempReceiptPayments { get; set; }
//        public ISettlementMongoRepository SettlementMongoRepository { get; }

//        //public IReceiptMongoRepository ReceiptRepository { get; }

//        public async Task<bool> CompleteAsync()
//        {
//            var changeAmount = await _context.SaveChangesAsync();

//            return changeAmount > 0;
//        }

//        public void Dispose()
//        {
//            _context.Dispose();
//        }
//    }
//}
