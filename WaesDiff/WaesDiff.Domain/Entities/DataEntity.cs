namespace WaesDiff.Domain.Entities
{

    using MongoDB.Bson.Serialization.Attributes;
    using WaesDiff.Domain.Enum;

    public class DataEntity
    {
        /// <summary>
        /// Unique id for to be the PK on the Mongo
        /// </summary>
        [BsonId]
        public string _id => string.Concat(Id.ToString(), EnumDataType);

        /// <summary>
        /// Id for the diff between data
        /// </summary>
        [BsonElement("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Data in string format
        /// </summary>
        [BsonElement("Data")]
        public string Data { get; set; }

        /// <summary>
        /// Data converted to Byte base64
        /// </summary>
        [BsonElement("DataBase64")]
        public byte[] DataBase64 { get; set; }

        /// <summary>
        /// Type of the data, left or right
        /// </summary>
        [BsonElement("EnumDataType")]
        public EnumDataType EnumDataType { get; set; }
    }
}
