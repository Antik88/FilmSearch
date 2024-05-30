using FakeItEasy;
using FilmoSearch.Controllers;
using FilmoSearch.DTOs.UserDtos;
using FilmoSearch.Services.AuthService;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmoSearch.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly IAuthService _authService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authService = A.Fake<IAuthService>();
            _controller = new AuthController(_authService);
        }

        [Fact]
        public async Task Register_ReturnsToken()
        {
            // Arrange
            var createUserDto = new CreateUserDto { Name = "testuser", Password = "password" };
            var token = "fake-jwt-token";
            A.CallTo(() => _authService.Register(createUserDto)).Returns(Task.FromResult(token));

            // Act
            var result = await _controller.Register(createUserDto);

            // Assert
            result.Value.Should().Be(token);
        }

        [Fact]
        public async Task Login_ReturnsToken()
        {
            // Arrange
            var createUserDto = new CreateUserDto { Name = "testuser", Password = "password" };
            var token = "fake-jwt-token";
            A.CallTo(() => _authService.Login(createUserDto)).Returns(Task.FromResult(token));

            // Act
            var result = await _controller.Login(createUserDto);

            // Assert
            result.Value.Should().Be(token);
        }

        [Fact]
        public async Task Check_ReturnsToken()
        {
            // Arrange
            var token = "fake-jwt-token";
            A.CallTo(() => _authService.CheckToken()).Returns(Task.FromResult(token));

            // Act
            var result = await _controller.Check();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().Be(token);
        }

        [Fact]
        public void GetName_ReturnsName()
        {
            // Arrange
            var username = "testuser";
            A.CallTo(() => _authService.GetName()).Returns(username);
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }))
                }
            };
            _controller.ControllerContext = controllerContext;

            // Act
            var result = _controller.GetName();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().Be(username);
        }
    }
}
