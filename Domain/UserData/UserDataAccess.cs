using Domain.User;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.UserData
{
    public class UserDataAccess
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime AllowedTill { get; set; }        
        public bool IsRequestedToDelete { get; set; }

        public UserDataAccess Update(string userId,DateTime allowedTill, bool isRequestedToDelete)
        {
            if (!string.IsNullOrEmpty(userId) && UserId?.Equals(userId) is not true) UserId = userId;
            if (IsRequestedToDelete.Equals(isRequestedToDelete) is not true) IsRequestedToDelete = isRequestedToDelete;
            if (allowedTill != DateTime.MinValue && AllowedTill.Equals(allowedTill) is not true) AllowedTill = allowedTill;

            return this;
        }
    }
}
