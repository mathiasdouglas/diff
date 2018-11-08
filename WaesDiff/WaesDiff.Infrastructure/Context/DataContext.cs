namespace WaesDiff.Infrastructure.Context
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Settings;

    /// <summary>
    /// Context of the DataEntity in the Mongo
    /// </summary>
    public sealed class DataContext : IMongoContext<DataEntity>
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<DataEntity> Collection { get; }

        public DataContext(IOptions<Settings> options)
        {
            var mongoSettings = options.Value.Mongo;

            var client = new MongoClient(mongoSettings.ConnectionString);
            Database = client.GetDatabase(mongoSettings.Database);

            Collection = Database.GetCollection<DataEntity>(mongoSettings.Collection);
        }
    }
}
