using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Core.Common.AppSettings
{
    public class MongoDbSettings
    {
        public string ReceiptPaymentCollectionName { get; set; }

        public string MongoConnectionString { get; set; }

        public string MongoDatabaseName { get; set; }
    }
}
 