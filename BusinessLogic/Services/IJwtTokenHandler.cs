using DataAccess.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IJwtTokenHandler
    {
        UserSession? GenerateJwtToken(string userName, string password);
    }
}
