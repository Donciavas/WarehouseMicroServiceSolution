namespace DataAccess.AuthModels
{
    public class UserAccount
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Role { get; set; }
    }
}
