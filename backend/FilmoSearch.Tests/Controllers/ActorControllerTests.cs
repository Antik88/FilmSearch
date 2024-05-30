using FakeItEasy;
using FilmoSearch.Controllers;
using FilmoSearch.DTOs.ActorDtos;
using FilmoSearch.Services.ActorService;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FilmoSearch.Tests.Controllers
{
    public class ActorControllerTests
    {
        private readonly IActorService _actorService;
        private readonly ActorController _controller;

        public ActorControllerTests()
        {
            _actorService = A.Fake<IActorService>();
            _controller = new ActorController(_actorService);
        }

        [Fact]
        public void ActorController_GetAllActors_ReturnOk()
        {
            //Arrange
            var controller = new ActorController(_actorService);

            //Act
            var result = controller.GetAllActors();

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSingleActor_ReturnsOk()
        {
            // Arrange
            var actorId = 1;
            var actorDto = new ActorFullDto();
            A.CallTo(() => _actorService.GetSingleActor(actorId)).Returns(actorDto);

            // Act
            var result = await _controller.GetSingleActor(actorId);

            // Assert
            result.Value.Should().Be(actorDto);
        }

        [Fact]
        public async Task AddActor_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var actorDto = new ActorDto();
            A.CallTo(() => _actorService.AddActor(actorDto)).Returns(actorDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "${AppSettings:AdminRole}") }))
            };

            // Act
            var result = await _controller.AddActor(actorDto);

            // Assert
            result.Value.Should().Be(actorDto);
        }

        [Fact]
        public async Task AddActor_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var actorDto = new ActorDto();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.AddActor(actorDto);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task UploadImage_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var actorId = 1;
            var imagePath = "path/to/image.jpg";
            A.CallTo(() => _actorService.UploadImage(A<IFormFile>.Ignored, actorId)).Returns(imagePath);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "${AppSettings:AdminRole}") }))
            };

            // Act
            var result = await _controller.UploadImage(A.Fake<IFormFile>(), actorId);

            // Assert
            result.Value.Should().Be(imagePath);
        }

        [Fact]
        public async Task UploadImage_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var actorId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.UploadImage(A.Fake<IFormFile>(), actorId);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateActor_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var actorId = 1;
            var actorDto = new UpdateActorDto();
            A.CallTo(() => _actorService.UpdateActor(actorId, actorDto)).Returns(actorDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "${AppSettings:AdminRole}") }))
            };

            // Act
            var result = await _controller.UpdateActor(actorId, actorDto);

            // Assert
            result.Value.Should().Be(actorDto);
        }

        [Fact]
        public async Task UpdateActor_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var actorId = 1;
            var actorDto = new UpdateActorDto();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.UpdateActor(actorId, actorDto);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteActor_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var actorId = 1;
            var actorDto = new ActorDto();
            A.CallTo(() => _actorService.DeleteActor(actorId)).Returns(actorDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "${AppSettings:AdminRole}") }))
            };

            // Act
            var result = await _controller.DeleteActor(actorId);

            // Assert
            result.Value.Should().Be(actorDto);
        }

        [Fact]
        public async Task DeleteActor_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var actorId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.DeleteActor(actorId);

            // Assert
            result.Result.Should().BeNull();
        }
    }
}
