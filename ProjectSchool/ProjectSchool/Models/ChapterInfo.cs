using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectSchool.Models
{
    public class ChapterInfo : BaseModelInfo
    {
        public ObjectId ComicId { get; set; }
        public string ComicName { get; set; }
        [BsonIgnore]
        public string ComicIdStr
        {
            get
            {
                return ComicId.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ComicId = new ObjectId(value);
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
    }
}
