namespace WaesDiff.Infrastructure.Repository
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WaesDiff.Infrastructure.Context;

    /// <summary>
    /// Main class to to the basic CRUD on the database
    /// </summary>
    /// <typeparam name="T">Entity that will be passed to the mongo</typeparam>
    public abstract class MongoRepository<T> where T : class
    {
        private readonly IMongoContext<T> _mongoContext;

        protected MongoRepository(IMongoContext<T> mongoContext)
        {
            _mongoContext = mongoContext;
        }

        /// <summary>
        /// Find an item in the Collection by filter
        /// </summary>
        /// <typeparam name="T">Type of the Entity</typeparam>
        /// <param name="filter">Filters to apply on find</param>
        public async Task<List<T>> Find(FilterDefinition<T> filter)
        {
            IAsyncCursor<T> task = await GetCollection().FindAsync(filter).ConfigureAwait(false);
            List<T> list = await task.ToListAsync().ConfigureAwait(false);
            return list;
        }

        /// <summary>
        /// Save a specific item on the collection
        /// </summary>
        /// <typeparam name="T">Type of the item to be saved</typeparam>
        /// <param name="filter">Filter to apply a item to be saved</param>
        /// <param name="replacement">Item to save</param>
        public async Task<ReplaceOneResult> Save(FilterDefinition<T> filter, T replacement)
        {
            UpdateOptions options = new UpdateOptions { IsUpsert = true };

            return await GetCollection().ReplaceOneAsync(filter, replacement, options).ConfigureAwait(false);
        }

        /// <summary>
        /// Get collection in database
        /// </summary>
        private IMongoCollection<T> GetCollection()
        {
            MongoCollectionSettings collectionSettings = new MongoCollectionSettings { GuidRepresentation = GuidRepresentation.Standard };
            return _mongoContext.Database.GetCollection<T>(_mongoContext.Collection.CollectionNamespace.CollectionName, collectionSettings);
        }
    }
}
