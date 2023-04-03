using AutoFixture;
using AutoFixture.Xunit2;
using BusinessLogic.Services;
using DataAccess.AuthModels;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Moq;
using WarehouseMicroserviceSolution.Tests.Customization.SpecimenBuilders;

namespace WarehouseMicroserviceSolution.Tests
{
    public class UserAccountServiceTest
    {
        [Theory, AutoData]
        public async Task UserAccountService_Signup_Returns_True_When_User_In_Database_Not_Found(string username, string password)
        {
            var userAccountRepositoryMoq = new Mock<IUserAccountRepository>();
            var sut = new UserAccountService(userAccountRepositoryMoq.Object);
            userAccountRepositoryMoq.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync((UserAccount)null!);

            var response = await sut.Signup(username, password);
            var message = response.Message;

            Assert.True(response.IsSuccess);
            Assert.Equal("User was created", message);
        }
        [Theory, AutoData]
        public async Task UserAccountService_Signup_Returns_False_When_User_In_Database_Found(string username, string password, UserAccount user)
        {
            var userAccountRepositoryMoq = new Mock<IUserAccountRepository>();
            var sut = new UserAccountService(userAccountRepositoryMoq.Object);
            userAccountRepositoryMoq.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync(user);

            var response = await sut.Signup(username, password);
            var message = response.Message;

            Assert.False(response.IsSuccess);
            Assert.Equal("User already exists", message);
        }
        [Fact]
        public async Task UserAccountService_Login_Returns_RespnseDto_IsSuccess_True_When_User_In_Database_And_password_OK()
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new UserAccountSpecimenBuilder());
            var user = fixture.Create<UserAccount>();

            var userAccountRepositoryMoq = new Mock<IUserAccountRepository>();
            var sut = new UserAccountService(userAccountRepositoryMoq.Object);
            userAccountRepositoryMoq.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync(user);
            
            var response = await sut.Login("admin", "test123")!;
            var message = response.Message;

            Assert.IsType<ResponseDto>(response);
            Assert.Equal("User credentials accepted", message);
        }
        [Theory, AutoData]
        public async Task UserAccountService_Login_Returns_ResponseDto_IsSuccess_False_When_User_In_Database_Found_But_password_Wrong(string username, string password)
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new UserAccountSpecimenBuilder());
            var user = fixture.Create<UserAccount>();

            var userAccountRepositoryMoq = new Mock<IUserAccountRepository>();
            var sut = new UserAccountService(userAccountRepositoryMoq.Object);
            userAccountRepositoryMoq.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync(user);
            var response = await sut.Login(username, password)!;
            var isSuccess = response.IsSuccess;

            Assert.False(isSuccess);
            Assert.Equal("Username or Password does not match", response.Message);
        }
        [Theory, AutoData]
        public async Task UserAccountService_Login_Returns_ReponseDto_IsSuccess_False_When_User_In_Database_Not_Found(string username, string password)
        {
            var userAccountRepositoryMoq = new Mock<IUserAccountRepository>();
            var sut = new UserAccountService(userAccountRepositoryMoq.Object);
            userAccountRepositoryMoq.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync((UserAccount)null!);
            
            var response = await sut.Login(username, password)!;
            var isSuccess = response.IsSuccess;

            Assert.False(isSuccess);
            Assert.Equal("Invalid Username or Password", response.Message);
        }
    }
}