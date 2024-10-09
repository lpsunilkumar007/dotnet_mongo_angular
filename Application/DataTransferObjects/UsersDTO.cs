namespace Application.DataTransferObjects
{
    public class UsersDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public int RemainingDays { get; set; }
    }
}
