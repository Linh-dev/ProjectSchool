using MongoDB.Driver;
using ProjectSchool.Models;

namespace ProjectSchool.Dao
{
    public class ComicDao : BaseDao<ComicInfo>
    {
        private const string COLLECTION = "Comic";
        public ComicDao() : base(COLLECTION)
        {
        }

        private static ComicDao _instance;
        public static ComicDao GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ComicDao();
            }
            return _instance;
        }

        public ComicInfo GetByTitle(string title)
        {
            var filter = Builders<ComicInfo>.Filter.Eq(o => o.IsDeleted, false);
            filter &= Builders<ComicInfo>.Filter.Eq(o => o.Title, title);

            var collection = GetCollection();

            return collection.Find(filter).FirstOrDefault();
        }
    }
}
