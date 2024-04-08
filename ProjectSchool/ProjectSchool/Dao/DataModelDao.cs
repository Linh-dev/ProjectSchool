using MongoDB.Driver;
using ProjectSchool.Models;

namespace ProjectSchool.Dao
{
    public class DataModelDao : BaseDao<DataModel>
    {
        private const string COLLECTION = "DataModel";
        public DataModelDao() : base(COLLECTION)
        {
        }

        private static DataModelDao _instance;
        public static DataModelDao GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataModelDao();
            }
            return _instance;
        }

        public DataModel GetByTitle(string title)
        {
            var filter = Builders<DataModel>.Filter.Eq(o => o.IsDeleted, false);
            filter &= Builders<DataModel>.Filter.Eq(o => o.Title, title);

            var collection = GetCollection();

            return collection.Find(filter).FirstOrDefault();
        }
    }
}
