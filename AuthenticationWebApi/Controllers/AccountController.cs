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
        private readonly UserAccountService _userAccountService;
        public AccountController(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public ActionResult<bool> Signup([FromBody] CredentialRequestDto registerRequest)
        {
            var response = _userAccountService.Signup(registerRequest.UserName!, registerRequest.Password!);
            if (response is false)
                return BadRequest("Choose different user name.");
            return Ok();
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<UserSession> Login([FromBody] CredentialRequestDto loginRequest)
        {
            var jwtAuthenticationManager = new JwtTokenHandler(_userAccountService);
            var userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.UserName!, loginRequest.Password!);
            if (userSession is null)
                return Unauthorized();
            else
                return userSession;
        }
    }
}
