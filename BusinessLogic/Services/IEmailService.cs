using DataAccess.DTOs;

namespace BusinessLogic.Services
{
    public interface IEmailService
    {
        ResponseDto GreetingEmail(string request);
    }
}
