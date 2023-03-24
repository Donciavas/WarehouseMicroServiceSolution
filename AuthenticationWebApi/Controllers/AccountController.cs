using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        public AccountController(IUserAccountService userAccountService, IJwtTokenHandler jwtTokenHandler)
        {
            _userAccountService = userAccountService;
            _jwtTokenHandler = jwtTokenHandler;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public ActionResult<bool> Signup([FromBody] CredentialRequestDto registerRequest)
        {
            var response = _userAccountService.Signup(registerRequest.UserName!, registerRequest.Password!);
            if (response is false)
                return BadRequest("Choose different user name.");
            return Ok("Account successfully created.");
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<UserSession> Login([FromBody] CredentialRequestDto loginRequest)
        {
            var userSession = _jwtTokenHandler.GenerateJwtToken(loginRequest.UserName!, loginRequest.Password!);
            if (userSession is null)
                return Unauthorized("Invalid username or password!");
            else
                return userSession;
        }
    }
}
