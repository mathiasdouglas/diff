using WaesDiff.Domain.Entities;

namespace WaesDiff.Infrastructure.Repository
{
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WaesDiff.Infrastructure.Context;

    public class JsonRepository : MongoRepository<JsonEntity>, IJsonRepository
    {
        public JsonRepository(IMongoContext<JsonEntity> mongoContext)
            : base(mongoContext)
        {
        }

        public async Task Save(JsonEntity jsonEntity)
        {
            FilterDefinition<JsonEntity> filter = Builders<JsonEntity>.Filter.Eq(o => o.Id, jsonEntity.Id);
            filter &= Builders<JsonEntity>.Filter.Eq(o => o.JsonType, jsonEntity.JsonType);

            await this.Save(filter, jsonEntity);
        }

        public async Task<List<JsonEntity>> Get(int id)
        {
            FilterDefinition<JsonEntity> filter = Builders<JsonEntity>.Filter.Eq(o => o.Id, id);
            var result = await Find(filter);

            return result;
        }
    }
}
