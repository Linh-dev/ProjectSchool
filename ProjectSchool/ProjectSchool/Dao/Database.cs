using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ProjectSchool.Models;

namespace ProjectSchool.Dao
{
    public class Database
    {
        private static MongoClient client;

        private static MongoClient GetMongoClient()
        {
            if (client == null)
            {
                var mongoDbConfiguration = new MongoDbConfiguration();
                client = new MongoClient("mongodb://localhost:27017");
            }

            return client;
        }

        public static IMongoDatabase GetDatabase()
        {

            //them tinh nang bo qua cac truong co trong db nhung khong co trong mo ta class
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true)
            };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);

            var db = GetMongoClient().GetDatabase("ProjectSchoolStore");
            return db;
        }
    }
}
