﻿using WaesDiff.Domain.Entities;

namespace WaesDiff.Infrastructure.Repository
{
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WaesDiff.Infrastructure.Context;

    /// <summary>
    /// Repository of the DataEntity
    /// </summary>
    public class DataRepository : MongoRepository<DataEntity>, IDataRepository
    {
        public DataRepository(IMongoContext<DataEntity> mongoContext)
            : base(mongoContext)
        {
        }

        public async Task Save(DataEntity dataEntity)
        {
            FilterDefinition<DataEntity> filter = Builders<DataEntity>.Filter.Eq(o => o._id, dataEntity._id);

            await this.Save(filter, dataEntity).ConfigureAwait(false);
        }

        public async Task<List<DataEntity>> Get(int id)
        {
            FilterDefinition<DataEntity> filter = Builders<DataEntity>.Filter.Eq(o => o.Id, id);
            var result = await Find(filter).ConfigureAwait(false);

            return result;
        }
    }
}
