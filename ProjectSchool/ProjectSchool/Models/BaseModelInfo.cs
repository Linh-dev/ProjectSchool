using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ProjectSchool.Extensions;

namespace ProjectSchool.Models
{
    [Serializable]
    public class BaseModelInfo
    {
        public ObjectId _id { get; set; }
        public ObjectId DomainId { get; set; }
        public bool IsDeleted { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreatedDate { get; set; }
        public ObjectId CreateBy { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdateDate { get; set; }
        public ObjectId UpdateBy { get; set; }
        public ObjectId DeleteBy { get; set; }

        #region ignore
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
        [BsonIgnore]
        public string CreateByStr
        {
            get
            {
                return CreateBy.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    CreateBy = new ObjectId(value);
                }
            }
        }
        [BsonIgnore]
        public string CreatedDateStr
        {
            get
            {
                return DateUtil.DateToString(CreatedDate);
            }
            set
            {
                CreatedDate = DateUtil.StringToDate(value);
            }
        }
        [BsonIgnore]
        public string UpdateByStr
        {
            get
            {
                return UpdateBy.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    UpdateBy = new ObjectId(value);
                }
            }
        }
        [BsonIgnore]
        public string UpdateDateStr
        {
            get
            {
                return DateUtil.DateToString(UpdateDate);
            }
            set
            {
                UpdateDate = DateUtil.StringToDate(value);
            }
        }
        [BsonIgnore]
        public string DeleteByStr
        {
            get
            {
                return DeleteBy.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DeleteBy = new ObjectId(value);
                }
            }
        }
        #endregion
    }
}
