using FakeItEasy;
using FilmoSearch.Controllers;
using FilmoSearch.DTOs.GenreDtos;
using FilmoSearch.Helpers;
using FilmoSearch.Services.GenreService;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmoSearch.Tests.Controllers
{
    public class GenreControllerTests
    {
        private readonly IGenreService _genreService;
        private readonly GenreController _controller;

        public GenreControllerTests()
        {
            _genreService = A.Fake<IGenreService>();
            _controller = new GenreController(_genreService);
        }

        [Fact]
        public async Task GetAllGenres_ReturnsOk()
        {
            // Arrange
            var genreList = new List<GenreDto> { new GenreDto(), new GenreDto() };
            A.CallTo(() => _genreService.GetAllGenres()).Returns(genreList);

            // Act
            var result = await _controller.GetAllGenres();

            // Assert
            result.Value.Should().BeEquivalentTo(genreList);
        }

        [Fact]
        public async Task GetSingleGenre_ReturnsOk()
        {
            // Arrange
            var genreId = 1;
            var genreDto = new GenreDto();
            A.CallTo(() => _genreService.GetSingleGenre(genreId)).Returns(genreDto);

            // Act
            var result = await _controller.GetSingleGenre(genreId);

            // Assert
            result.Value.Should().Be(genreDto);
        }

        [Fact]
        public async Task GetSingleGenre_ReturnsNotFound()
        {
            // Arrange
            var genreId = 1;
            A.CallTo(() => _genreService.GetSingleGenre(genreId)).Returns<GenreDto>(null);

            // Act
            var result = await _controller.GetSingleGenre(genreId);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task AddGenre_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var genreDto = new CreateGenreDto();
            A.CallTo(() => _genreService.AddGenre(genreDto)).Returns(genreDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.AddGenre(genreDto);

            // Assert
            result.Value.Should().Be(genreDto);
        }

        [Fact]
        public async Task AddGenre_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var genreDto = new CreateGenreDto();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.AddGenre(genreDto);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateGenre_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var genreId = 1;
            var genreDto = new UpdateGenreDto();
            A.CallTo(() => _genreService.UpdateGenre(genreId, genreDto)).Returns(genreDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.UpdateGenre(genreId, genreDto);

            // Assert
            result.Value.Should().Be(genreDto);
        }

        [Fact]
        public async Task UpdateGenre_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var genreId = 1;
            var genreDto = new UpdateGenreDto();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.UpdateGenre(genreId, genreDto);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteGenre_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var genreId = 1;
            var genreDto = new GenreDto();
            A.CallTo(() => _genreService.DeleteGenre(genreId)).Returns(genreDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.DeleteGenre(genreId);

            // Assert
            result.Value.Should().Be(genreDto);
        }

        [Fact]
        public async Task DeleteGenre_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var genreId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.DeleteGenre(genreId);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteGenre_ReturnsNotFound()
        {
            // Arrange
            var genreId = 1;
            A.CallTo(() => _genreService.DeleteGenre(genreId)).Returns<GenreDto>(null);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.DeleteGenre(genreId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
