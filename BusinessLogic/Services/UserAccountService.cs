using DataAccess.AuthModels;
using DataAccess.DTOs;
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
        public async Task<ResponseDto> Signup(string username, string password)
        {
            var user = await _userAccountRepository.GetUser(username);
            if (user is not null)
                return new ResponseDto(false, "User already exists");
            user = CreateUser(username, password);
            await _userAccountRepository.SaveUser(user);
            return new ResponseDto(true, "User was created");
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
        public async Task<ResponseDto>? Login(string userName, string password)
        {
            var account = await _userAccountRepository.GetUser(userName);
            if (account is null)
                return new ResponseDto(false, "Invalid Username or Password");
            if (!VerifyPasswordHash(password, account.Password!, account.PasswordSalt!))
                return new ResponseDto(false, "Username or Password does not match");
            return new ResponseDto(true, "User credentials accepted");
        }
        public async Task<UserAccount> GetUserAccount(string username)
        {
            var user = await _userAccountRepository.GetUser(username);
            return user;
        }
    }
}
