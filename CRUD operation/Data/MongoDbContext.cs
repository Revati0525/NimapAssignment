using MongoDB.Driver;
using CRUD_operation.Models;

namespace CRUD_operation.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        // Constructor now reads the connection string and database name directly from the configuration
        public MongoDbContext(IConfiguration config)
        {
            // Read the connection string from the configuration
            var connectionString = config.GetValue<string>("MongoDbSettings:ConnectionString");
            var databaseName = config.GetValue<string>("MongoDbSettings:DatabaseName");

            // Check if the connection string or database name is null
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "MongoDB connection string cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException("databaseName", "MongoDB database name cannot be null or empty.");
            }

            // Initialize the MongoClient with the connection string
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Get the categories and products collections
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    }
}
