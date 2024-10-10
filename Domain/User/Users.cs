using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.User
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }   

    }
}
