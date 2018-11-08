namespace WaesDiff.Infrastructure.Context
{
    using MongoDB.Driver;

    public interface IMongoContext<T>
    {
        /// <summary>
        /// Database of the Mongo
        /// </summary>
        IMongoDatabase Database { get; }

        /// <summary>
        /// Collection of the database
        /// </summary>
        IMongoCollection<T> Collection { get; }
    }
}
