using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarShield.API.Models
{
    public class User
    {
        [BsonId] // Đánh dấu đây là khóa chính _id của Mongo
        [BsonRepresentation(BsonType.ObjectId)] // Tự động chuyển ObjectId sang string
        public string? Id { get; set; }

        [BsonElement("username")] // Map với field "username" trong DB
        public string Username { get; set; } = null!;

        [BsonElement("password_hash")]
        public string PasswordHash { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("marshmallow_money")]
        public decimal MarshmallowMoney { get; set; } // Dùng decimal cho tiền tệ là chuẩn nhất

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("is_active")]
        public bool IsActive { get; set; } = true;
    }
}
