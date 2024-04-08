using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectSchool.Models
{
    public class ChapterModel : BaseModelInfo
    {
        public ObjectId DataId { get; set; }
        [BsonIgnore]
        public string DataIdStr
        {
            get
            {
                return DataId.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DataId = new ObjectId(value);
                }
            }
        }
        public string MyProperty { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public List<string> Base64Images { get; set; }
        public bool IsDeleted { get; set; }
    }
}
