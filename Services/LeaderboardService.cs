using MarShield.API.Models;
using MongoDB.Driver;

namespace MarShield.API.Services
{
    public class LeaderboardService
    {
        private readonly IMongoCollection<Leaderboard> _collection;
        public LeaderboardService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetValue<string>("MarShieldDatabase:ConnectionString"));
            var mongoDatabase = mongoClient.GetDatabase(config.GetValue<string>("MarShieldDatabase:DatabaseName"));
            _collection = mongoDatabase.GetCollection<Leaderboard>(config.GetValue<string>("MarShieldDatabase:LeaderboardsCollectionName"));
        }

        // Get top 10 players by TotalScore
        public async Task<List<Leaderboard>> GetTopPlayersAsync() =>
            await _collection.Find(_ => true)
                             .SortByDescending(x => x.TotalScore)
                             .Limit(10)
                             .ToListAsync();

        public async Task CreateOrUpdateAsync(Leaderboard entry)
        {
            // Logic: Nếu User đã có trong bảng xếp hạng thì update, chưa có thì tạo mới
            var filter = Builders<Leaderboard>.Filter.Eq(x => x.UserId, entry.UserId);
            var options = new ReplaceOptions { IsUpsert = true };
            await _collection.ReplaceOneAsync(filter, entry, options);
        }
    }
}
