using MongoDB.Driver;
using ProjectSchool.Models;
using System.Reflection;

namespace ProjectSchool.Dao
{
    public class ChapterDao : BaseDao<ChapterInfo>
    {
        private const string COLLECTION = "Chapter";
        public ChapterDao() : base(COLLECTION)
        {
        }

        private static ChapterDao _instance;
        public static ChapterDao GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ChapterDao();
            }
            return _instance;
        }

        public ChapterInfo GetByName(string chapterName)
        {
            var filter = Builders<ChapterInfo>.Filter.Eq(o => o.IsDeleted, false);
            filter &= Builders<ChapterInfo>.Filter.Eq(o => o.Name, chapterName);

            var collection = GetCollection();

            return collection.Find(filter).FirstOrDefault();
        }
    }
}
