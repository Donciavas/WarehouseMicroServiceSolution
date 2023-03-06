using JwtAuthenticationManager.Models;

namespace JwtAuthenticationManager
{
    public class UserAccountService
    {
        private List<UserAccount> _userAccountList;

        public UserAccountService()
        {
            _userAccountList = new List<UserAccount>
            {
                new UserAccount{ UserName = "admin", Password = "admin", Role = "Administrator" }
            };
        }

        public UserAccount? GetUserAccountTest(AuthenticationRequest authenticationRequest)
        {
            return _userAccountList.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
        }
        public UserAccount? GetUserAccount(string userName, string password)
        {
            return _userAccountList.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
        }
    }
}
