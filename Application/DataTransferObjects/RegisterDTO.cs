namespace Application.DataTransferObjects
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsTermAndPolicyAccepted { get; set; }
    }
}
