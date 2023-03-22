using DataAccess.AuthModels;
using DataAccess.MicroServiceDbContexts;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserAccountDbContext _userAccountDbContext;
        private readonly ILogger<UserAccountRepository> _logger;
        public UserAccountRepository(UserAccountDbContext context, ILogger<UserAccountRepository> logger)
        {
            _userAccountDbContext = context;
            _logger = logger;
        }
        public UserAccount GetUser(string username)
        {
            var user = _userAccountDbContext.UserAccounts!.SingleOrDefault(x => x.UserName == username);
            if (user is null)
                return default!;
            return user;
        }
        public async Task<bool> SaveUser(UserAccount user)
        {
            try
            {
                await _userAccountDbContext.UserAccounts!.AddAsync(user);
                await _userAccountDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
