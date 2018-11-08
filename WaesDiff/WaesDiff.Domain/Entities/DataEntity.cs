namespace WaesDiff.Domain.Entities
{

    using MongoDB.Bson.Serialization.Attributes;
    using WaesDiff.Domain.Enum;

    public class DataEntity
    {
        [BsonId]
        public string _id => string.Concat(Id.ToString(), DataType);

        [BsonElement("Id")]
        public int Id { get; set; }

        [BsonElement("Data")]
        public string Data { get; set; }

        [BsonElement("DataBase64")]
        public byte[] DataBase64 { get; set; }

        [BsonElement("DataType")]
        public DataType DataType { get; set; }
    }
}
