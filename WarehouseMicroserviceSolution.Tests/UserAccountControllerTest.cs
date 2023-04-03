using AuthenticationWebApi.Controllers;
using AutoFixture;
using AutoFixture.Xunit2;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.AuthModels;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WarehouseMicroserviceSolution.Tests.Customization.SpecimenBuilders;

namespace WarehouseMicroserviceSolution.Tests
{
    public class UserAccountControllerTest
    {
        [Theory, AutoData]
        public async Task AccountController_Signup_Returns_StatusCode_200OK(CredentialRequestDto registerRequest)
        {
            var jwtServiceMoq = new Mock<IJwtTokenHandler>();
            var userAccountServiceMoq = new Mock<IUserAccountService>();
            var sut = new AccountController(userAccountServiceMoq.Object, jwtServiceMoq.Object);
            userAccountServiceMoq.Setup(x => x.Signup(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseDto(true, "User was created"));

            var response = await sut.Signup(registerRequest);
            
            Assert.IsType<OkObjectResult>(response.Result);
        }
        [Theory, AutoData]
        public async Task AccountController_Signup_Returns_BadRequest(CredentialRequestDto registerRequest)
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new UserAccountSpecimenBuilder());
            var resposseDto = fixture.Create<UserAccount>();
            var jwtServiceMoq = new Mock<IJwtTokenHandler>();
            var userAccountServiceMoq = new Mock<IUserAccountService>();
            var sut = new AccountController(userAccountServiceMoq.Object, jwtServiceMoq.Object);
            userAccountServiceMoq.Setup(x => x.Signup(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseDto(false, "User already exists"));

            var response = await sut.Signup(registerRequest);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
        [Theory, AutoData]
        public async Task AccountController_Login_Returns_StatusCode_200OK_UserSession(CredentialRequestDto loginRequest)
        {
            var fixture = new Fixture();
            var jwtServiceMoq = new Mock<IJwtTokenHandler>();
            var userAccountServiceMoq = new Mock<IUserAccountService>();
            fixture.Customizations.Add(new UserSessionSpecimenBuilder());
            var responseDto = fixture.Create<UserSession>();
            var sut = new AccountController(userAccountServiceMoq.Object, jwtServiceMoq.Object);
            userAccountServiceMoq.Setup(x => x.Login(loginRequest.UserName!, loginRequest.Password!)).ReturnsAsync(new ResponseDto(true, "User credentials accepted"));
            jwtServiceMoq.Setup(y => y.GenerateJwtToken(loginRequest.UserName!)).ReturnsAsync(responseDto);

            var response = await sut.Login(loginRequest);

            Assert.IsType<OkObjectResult>(response.Result);
            Assert.IsType<UserSession>(responseDto);
        }
        [Theory, AutoData]
        public async Task AccountController_Login_Returns_Unauthorized_BadCredentials(CredentialRequestDto loginRequest)
        {
            var jwtServiceMoq = new Mock<IJwtTokenHandler>();
            var userAccountServiceMoq = new Mock<IUserAccountService>();
            var sut = new AccountController(userAccountServiceMoq.Object, jwtServiceMoq.Object);
            userAccountServiceMoq.Setup(y => y.Login(loginRequest.UserName!, loginRequest.Password!)).ReturnsAsync(new ResponseDto(false, "Username or Password does not match"));
            
            var response = await sut.Login(loginRequest);

            Assert.IsType<UnauthorizedObjectResult>(response.Result);
        }
        [Theory, AutoData]
        public async Task AccountController_Login_Returns_BadRequest_InternalServerError(CredentialRequestDto loginRequest)
        {
            var fixture = new Fixture();
            var jwtServiceMoq = new Mock<IJwtTokenHandler>();
            var userAccountServiceMoq = new Mock<IUserAccountService>();
            fixture.Customizations.Add(new UserSessionSpecimenBuilder());
            var responseDto = fixture.Create<UserSession>();
            var sut = new AccountController(userAccountServiceMoq.Object, jwtServiceMoq.Object);
            userAccountServiceMoq.Setup(x => x.Login(loginRequest.UserName!, loginRequest.Password!)).ReturnsAsync(new ResponseDto(true, "User credentials accepted"));
            jwtServiceMoq.Setup(y => y.GenerateJwtToken(loginRequest.UserName!)).ReturnsAsync((UserSession)null!);

            var response = await sut.Login(loginRequest);

            Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.IsType<UserSession>(responseDto);
        }
    }
}