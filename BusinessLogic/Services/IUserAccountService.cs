using DataAccess.AuthModels;
using DataAccess.DTOs;

namespace BusinessLogic.Services
{
    public interface IUserAccountService
    {
        Task<ResponseDto> Signup(string username, string password);
        Task<ResponseDto>? Login(string userName, string password);
        Task<UserAccount> GetUserAccount(string username);
    }
}
