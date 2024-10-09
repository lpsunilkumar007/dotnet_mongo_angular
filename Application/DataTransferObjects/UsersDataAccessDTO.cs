namespace Application.DataTransferObjects
{
    public class UsersDataAccessDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }        
        public DateTime AllowedTill { get; set; }
        public bool IsRequestedToDelete { get; set; }
    }
}
