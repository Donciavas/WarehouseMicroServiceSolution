using DataAccess.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IUserAccountService
    {
        bool Signup(string username, string password);
        UserAccount? Login(string userName, string password);
    }
}
