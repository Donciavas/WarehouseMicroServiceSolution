using DataAccess.AuthModels;

namespace BusinessLogic.Services
{
    public interface IUserAccountService
    {
        bool Signup(string username, string password);
        UserAccount? Login(string userName, string password);
    }
}
