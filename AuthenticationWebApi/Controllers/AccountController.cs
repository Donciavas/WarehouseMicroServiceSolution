using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
=======
>>>>>>> 68bc333f127a027f9a4cf4a25ef82e7b064597f2
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
<<<<<<< HEAD
        private UserAccountService _userAccountService;

        public AccountController(JwtTokenHandler jwtTokenHandler, UserAccountService userAccountService)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _userAccountService = userAccountService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult<UserSession> Login([FromBody] LoginRequest loginRequest)
        {
            var jwtAuthenticationManager = new JwtTokenHandler(_userAccountService);
            var userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.UserName!, loginRequest.Password!);
            if (userSession is null)
                return Unauthorized();
            else
                return userSession;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse?> AuthenticateForTesting([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = _jwtTokenHandler.GenerateJwtTokenTest(authenticationRequest);
=======

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(authenticationRequest);
>>>>>>> 68bc333f127a027f9a4cf4a25ef82e7b064597f2
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }
    }
}
