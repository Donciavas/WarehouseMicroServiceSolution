using DataAccess.AuthModels;

namespace BusinessLogic.Services
{
    public interface IJwtTokenHandler
    {
        UserSession? GenerateJwtToken(string userName, string password);
    }
}
