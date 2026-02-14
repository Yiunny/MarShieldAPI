using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarShield.API.Models
{
    public class Leaderboard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId { get; set; } = null!;

        [BsonElement("username")]
        public string Username { get; set; } = null!; // Saving username without needing to join with Users collection

        [BsonElement("max_survival_days")]
        public int MaxSurvivalDays { get; set; }

        [BsonElement("total_score")]
        public int TotalScore { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
