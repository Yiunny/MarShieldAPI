using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarShield.API.Models
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId { get; set; } = null!;

        [BsonElement("items")]
        public List<InventoryItem> Items { get; set; } = new List<InventoryItem>();
    }
    public class InventoryItem
    {
        [BsonElement("item_id")]
        public string ItemId { get; set; } = null!;

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("acquired_at")]
        public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
    }
}
