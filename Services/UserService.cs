using MarShield.API.Models;
using MarShield.API.Helpers;
using MongoDB.Driver;

namespace MarShield.API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        public UserService(IConfiguration config)
        {
            // Get info from appsettings.json
            var connectionString = config.GetValue<string>("MarShieldDatabase:ConnectionString");
            var databaseName = config.GetValue<string>("MarShieldDatabase:DatabaseName");
            var collectionName = config.GetValue<string>("MarShieldDatabase:UsersCollectionName");

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(collectionName);
        }

        // Get all Users
        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        // Create a new User
        // Hash password before saving to DB
        public async Task CreateAsync(User newUser)
        {
            newUser.PasswordHash = SecurityHelper.HashPassword(newUser.PasswordHash);
            await _usersCollection.InsertOneAsync(newUser);
        }

        // Login method: verify username and password
        public async Task<User?> LoginAsync(string username, string password)
        {
            // Find user by username
            var user = await _usersCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

            if (user == null) return null; // Không tìm thấy user

            // Hashing password user just input
            string inputHash = SecurityHelper.HashPassword(password);

            // Compare hash from DB with hash of input password
            if (user.PasswordHash != inputHash) return null; // Wrong password

            return user; // Login successful
        }
        // Check if a user exists by username
        public async Task<bool> CheckUserExistsAsync(string username)
        {
            // Find if there is a user existence via CountDocuments
            var count = await _usersCollection.CountDocumentsAsync(x => x.Username == username);
            return count > 0;
        }
    }
}

