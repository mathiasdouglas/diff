namespace WaesDiff.Infrastructure.Context
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Settings;

    public sealed class JsonContext : IMongoContext<JsonEntity>
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<JsonEntity> Collection { get; }

        public JsonContext(IOptions<Settings> options)
        {
            var mongoSettings = options.Value.Mongo;

            var client = new MongoClient(mongoSettings.ConnectionString);
            Database = client.GetDatabase(mongoSettings.Database);

            Collection = Database.GetCollection<JsonEntity>(mongoSettings.Collection);
        }
    }
}
