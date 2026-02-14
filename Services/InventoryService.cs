using MarShield.API.Models;
using MongoDB.Driver;

namespace MarShield.API.Services
{
    public class InventoryService
    {
        private readonly IMongoCollection<Inventory> _collection;
        public InventoryService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetValue<string>("MarShieldDatabase:ConnectionString"));
            var mongoDatabase = mongoClient.GetDatabase(config.GetValue<string>("MarShieldDatabase:DatabaseName"));
            _collection = mongoDatabase.GetCollection<Inventory>(config.GetValue<string>("MarShieldDatabase:InventoriesCollectionName"));
        }

        public async Task<Inventory?> GetByUserIdAsync(string userId) =>
            await _collection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

        // Create Inventory (rarely used directly)
        public async Task CreateAsync(Inventory inventory) =>
            await _collection.InsertOneAsync(inventory);

        //Upsert (Update or Insert)
        public async Task AddItemAsync(string userId, InventoryItem item)
        {
            // 1. Find exaclty the inventory by UserId
            var filter = Builders<Inventory>.Filter.Eq(x => x.UserId, userId);

            // 2. Update:
            // - Push: Add new Item to Items array
            // - SetOnInsert: CHỈ KHI TẠO MỚI thì mới set UserId (để đảm bảo bản ghi mới có đủ info)
            var update = Builders<Inventory>.Update
                           .Push(x => x.Items, item)
                           .SetOnInsert(x => x.UserId, userId);

            // 3. Set IsUpsert = true
            var options = new UpdateOptions { IsUpsert = true };

            // Execution: If no inventory -> Create new one, attach to UserId, then add Items.
            //           Nếu túi có rồi -> Just add Items only.
            await _collection.UpdateOneAsync(filter, update, options);
        }
    }
}
