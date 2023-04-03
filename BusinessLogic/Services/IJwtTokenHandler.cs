using DataAccess.AuthModels;

namespace BusinessLogic.Services
{
    public interface IJwtTokenHandler
    {
        Task<UserSession>? GenerateJwtToken(string userName);
    }
}
