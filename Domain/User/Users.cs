using Domain.UserData;
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
        public Users Update(string firstName, string email, string lastName, string mobileNumber)
        {
            if (!string.IsNullOrEmpty(firstName) && FirstName?.Equals(firstName) is not true) FirstName = firstName;
            if (!string.IsNullOrEmpty(email) && Email?.Equals(email) is not true) Email = email;
            if (!string.IsNullOrEmpty(lastName) && LastName?.Equals(lastName) is not true) LastName = lastName;
            if (!string.IsNullOrEmpty(mobileNumber) && MobileNumber?.Equals(mobileNumber) is not true) MobileNumber = mobileNumber;
            return this;
        }

    }
}
