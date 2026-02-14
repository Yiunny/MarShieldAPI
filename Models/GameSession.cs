using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarShield.API.Models
{
    public class GameSession
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId { get; set; } = null!;

        [BsonElement("current_survival_day")]
        public int CurrentSurvivalDay { get; set; }

        [BsonElement("current_ingame_money")]
        public decimal CurrentIngameMoney { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "Playing"; // Playing, Finished, Died

        [BsonElement("last_synced_at")]
        public DateTime LastSyncedAt { get; set; } = DateTime.UtcNow;

        // Save snapshot data as a flexible object (to load/save various game state data)
        [BsonElement("snapshot_data")]
        public object? SnapshotData { get; set; }
    }
}
