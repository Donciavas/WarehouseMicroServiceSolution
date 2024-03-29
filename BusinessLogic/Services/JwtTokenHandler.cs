﻿using DataAccess.AuthModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Services
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "pRdCqn8cSWLtaJwbRg8jGzpQRyEA1gdXkt7GoPd4";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private IUserAccountService _userAccountService;
        public JwtTokenHandler(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        public async Task<UserSession>? GenerateJwtToken(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null!;
            var userAccount = await _userAccountService.GetUserAccount(userName)!;
            if (userAccount == null) return null!;
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.UserName!),
                new Claim("Role", userAccount.Role!)
            });
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            var userSession = new UserSession
            {
                UserName = userAccount.UserName,
                Role = userAccount.Role,
                Token = token,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
            return userSession;
        }
    }
}