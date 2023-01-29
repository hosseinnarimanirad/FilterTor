using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using SampleApp.Core.Common.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Api.Infrastructure.MongoRepositories
{
    public class MongoContext
    {
        private IMongoDatabase Database { get; set; }
         
        public MongoClient MongoClient { get; set; }

        private readonly List<Func<Task>> _asyncCommands;

        private readonly List<Action> _commands;

        static MongoContext()
        {
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
        }

        public MongoContext(MongoDbSettings settings) : this(settings.MongoConnectionString, settings.MongoDatabaseName)
        {

        }

        public MongoContext(string connectionString, string databaseName)
        {
            MongoClient = new MongoClient(connectionString);
             
            Database = MongoClient.GetDatabase(databaseName);
             
            //_configuration = configuration;

            // Every command will be stored and it'll be processed at SaveChanges
            _asyncCommands = new List<Func<Task>>();

            _commands = new List<Action>();
        }

        public async Task<int> SaveChangesAsync()
        {
            //ConfigureMongo();
            var result = 0;

            if (_commands?.Any() != true && _asyncCommands?.Any() != true)
            {
                return result;
            }

            //using (Session = await MongoClient.StartSessionAsync())
            //{
            try
            {
                //Session.StartTransaction();

                if (_asyncCommands?.Any() == true)
                {
                    var commandTasks = _asyncCommands.Select(c => c());

                    await Task.WhenAll(commandTasks);
                }

                if (_commands?.Any() == true)
                {
                    foreach (var command in _commands)
                    {
                        command();
                    }
                }

                //await Session.CommitTransactionAsync();

                result = _commands.Count;
            }
            catch (Exception ex)
            {
                //await Session.AbortTransactionAsync();
            }
            finally
            {
                _commands.Clear();

                _asyncCommands.Clear();
            }
            //}

            return result;
        }
         
        //private void ConfigureMongo()
        //{
        //    if (MongoClient != null)
        //        return;

        //    // Configure mongo (You can inject the config, just to simplify)
        //    MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);

        //    Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);

        //}

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            //ConfigureMongo();
            return Database.GetCollection<T>(name);
        }

        public void AddAsyncCommand(Func<Task> func)
        {
            _asyncCommands.Add(func);
        }

        public void AddCommand(Action action)
        {
            _commands.Add(action);
        }

        public void Dispose()
        {
            //Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
