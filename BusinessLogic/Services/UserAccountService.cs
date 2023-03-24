using DataAccess.AuthModels;
using DataAccess.Repositories;
using System.Security.Cryptography;

namespace BusinessLogic.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        public bool Signup(string username, string password)
        {
            var user = _userAccountRepository.GetUser(username);
            if (user is not null)
                return false;
            user = CreateUser(username, password);
            _userAccountRepository.SaveUser(user);
            return true;
        }
        private static UserAccount CreateUser(string username, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new UserAccount
            {
                Id = Guid.NewGuid(),
                UserName = username,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "Administrator"
            };
            return user;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        }
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
        public UserAccount? Login(string userName, string password)
        {
            var account = _userAccountRepository.GetUser(userName);
            if (account is null)
                return default;
            if (!VerifyPasswordHash(password, account.Password!, account.PasswordSalt!))
                return default;
            return account;
        }
    }
}
