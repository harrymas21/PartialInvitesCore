using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public string Name { get; set; }

        [BsonElement]
        public string Email { get; set; }

        [BsonElement]
        public string Phone { get; set; }

        [BsonElement]
        public bool? WillAttend { get; set; }
    }
}
