using DataAccess.AuthModels;
using DataAccess.DTOs;
using DataAccess.MicroServiceDbContexts;
using Microsoft.EntityFrameworkCore;
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
        public async Task<UserAccount> GetUser(string username)
        {
            try
            {
                var user = await _userAccountDbContext.UserAccounts!.SingleOrDefaultAsync(x => x.UserName == username);
                return user!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return default!;
            }
        }
        public async Task<ResponseDto> SaveUser(UserAccount user)
        {
            try
            {
                await _userAccountDbContext.UserAccounts!.AddAsync(user);
                await _userAccountDbContext.SaveChangesAsync();
                return new ResponseDto(true, "User was saved in the database");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ResponseDto(false, "Failed to save user in the database");
            }
        }
    }
}
