using MongoDB.Driver;
using ProjectSchool.Models;
using System.Reflection;

namespace ProjectSchool.Dao
{
    public class ChapterModelDao : BaseDao<ChapterModel>
    {
        private const string COLLECTION = "ChapterModel";
        public ChapterModelDao() : base(COLLECTION)
        {
        }

        private static ChapterModelDao _instance;
        public static ChapterModelDao GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ChapterModelDao();
            }
            return _instance;
        }

        public ChapterModel GetByName(string chapterName)
        {
            var filter = Builders<ChapterModel>.Filter.Eq(o => o.IsDeleted, false);
            filter &= Builders<ChapterModel>.Filter.Eq(o => o.Name, chapterName);

            var collection = GetCollection();

            return collection.Find(filter).FirstOrDefault();
        }
    }
}
