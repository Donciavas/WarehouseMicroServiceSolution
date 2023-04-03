using DataAccess.AuthModels;
using DataAccess.DTOs;

namespace DataAccess.Repositories
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> GetUser(string username);
        Task<ResponseDto> SaveUser(UserAccount user);
    }
}
