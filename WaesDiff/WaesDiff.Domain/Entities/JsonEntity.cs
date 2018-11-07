namespace WaesDiff.Domain.Entities
{

    using MongoDB.Bson.Serialization.Attributes;
    using WaesDiff.Domain.Enum;

    public class JsonEntity
    {
        [BsonId]
        public string _id => string.Concat(Id.ToString(), JsonType);

        [BsonElement("Id")]
        public int Id { get; set; }

        [BsonElement("Json")]
        public byte[] Json { get; set; }

        [BsonElement("JsonType")]
        public DiffType JsonType { get; set; }
    }
}
