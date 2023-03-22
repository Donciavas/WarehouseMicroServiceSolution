using DataAccess.AuthModels;

namespace DataAccess.Repositories
{
    public interface IUserAccountRepository
    {
        UserAccount GetUser(string username);
        Task<bool> SaveUser(UserAccount user);
    }
}
