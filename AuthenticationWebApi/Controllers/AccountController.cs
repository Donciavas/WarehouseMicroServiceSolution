using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.AuthModels;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public async Task<ActionResult<ResponseDto>> Signup([FromBody] CredentialRequestDto registerRequest)
        {
            var response = await _userAccountService.Signup(registerRequest.UserName!, registerRequest.Password!);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async  Task<ActionResult<(ResponseDto, UserSession)>> Login([FromBody] CredentialRequestDto loginRequest)
        {
            var response = await _userAccountService.Login(loginRequest.UserName!, loginRequest.Password!)!;
            if (!response.IsSuccess)
                return Unauthorized(response);
            var userSession = await _jwtTokenHandler.GenerateJwtToken(loginRequest.UserName!)!;
            if (userSession is null)
                return BadRequest(new ResponseDto(false, "Internal Server Error. Cannot log in at the moment"));
            else
                return Ok(userSession);
        }
    }
}
