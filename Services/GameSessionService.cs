using MarShield.API.Models;
using MongoDB.Driver;

namespace MarShield.API.Services
{
    public class GameSessionService
    {
        private readonly IMongoCollection<GameSession> _collection;
        public GameSessionService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetValue<string>("MarShieldDatabase:ConnectionString"));
            var mongoDatabase = mongoClient.GetDatabase(config.GetValue<string>("MarShieldDatabase:DatabaseName"));
            _collection = mongoDatabase.GetCollection<GameSession>(config.GetValue<string>("MarShieldDatabase:GameSessionsCollectionName"));
        }

        public async Task CreateAsync(GameSession session) =>
            await _collection.InsertOneAsync(session);

        public async Task<GameSession?> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        //Update Sync
        public async Task UpdateSyncAsync(string id, GameSession updatedSession) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedSession);
    }
}
