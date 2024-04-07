using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjectSchool.Models
{
    [Serializable]
    public class BaseModelInfo
    {
        public ObjectId _id { get; set; }

        public ObjectId DomainId { get; set; }


        [BsonIgnore]
        public string IdStr
        {
            get
            {
                return _id.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _id = new ObjectId(value);
                }
            }
        }

        [BsonIgnore]
        public string DomainIdStr
        {
            get
            {
                return DomainId.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DomainId = new ObjectId(value);
                }
            }
        }
    }
}
