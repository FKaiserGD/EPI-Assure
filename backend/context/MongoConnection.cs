using MongoDB.Driver;

namespace backend.context
{
    public class MongoConnection
    {
        public IMongoDatabase context;

        public MongoConnection (IConfiguration myConfig)
        {
            var ConnectionString = myConfig["ConnectionStrings:MyMongoConnection"];
            var DatabaseString = myConfig["MongoDatabases:EPIAssure:Name"];

            try
            {
                var MyMongoClient = new MongoClient(ConnectionString);
                context = MyMongoClient.GetDatabase(DatabaseString);
            }
            catch (Exception ex)
            {
                throw new MongoException($"Não foi possível conectar ao MongoDB: {ex.Message}");
            }
        }
    }
}