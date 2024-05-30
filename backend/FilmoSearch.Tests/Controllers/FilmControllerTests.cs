using FakeItEasy;
using FilmoSearch.Controllers;
using FilmoSearch.DTOs.Film;
using FilmoSearch.DTOs.FilmDtos;
using FilmoSearch.Helpers;
using FilmoSearch.Services.FilmService;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmoSearch.Tests.Controllers
{
    public class FilmControllerTests
    {
        private readonly IFilmService _filmService;
        private readonly FilmController _controller;

        public FilmControllerTests()
        {
            _filmService = A.Fake<IFilmService>();
            _controller = new FilmController(_filmService);
        }

        [Fact]
        public async Task GetAllFilms_ReturnsFilms()
        {
            // Arrange
            var films = new List<AllFilmsDto> { new AllFilmsDto { Id = 1, Title = "Film1" } };
            A.CallTo(() => _filmService.GetAllFilms(null, null, null)).Returns(films);

            // Act
            var result = await _controller.GetAllFilms(null, null, null);

            // Assert
            result.Value.Should().BeEquivalentTo(films);
        }

        [Fact]
        public async Task GetSingleFilm_ReturnsFilm()
        {
            // Arrange
            var filmId = 1;
            var film = new FilmDto { Id = filmId, Title = "Film1" };
            A.CallTo(() => _filmService.GetSingleFilm(filmId)).Returns(film);

            // Act
            var result = await _controller.GetSingleFilm(filmId);

            // Assert
            result.Value.Should().Be(film);
        }

        [Fact]
        public async Task GetSingleFilm_ReturnsNotFound()
        {
            // Arrange
            var filmId = 1;
            A.CallTo(() => _filmService.GetSingleFilm(filmId)).Returns(Task.FromResult<FilmDto>(null));

            // Act
            var result = await _controller.GetSingleFilm(filmId);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }


        [Fact]
        public async Task AddFilm_ReturnsCreatedFilm()
        {
            // Arrange
            var createFilmDto = new CreateFilmDto { Title = "New Film" };
            var filmDto = new FilmDto { Id = 1, Title = "New Film" };
            A.CallTo(() => _filmService.AddFilm(createFilmDto)).Returns(filmDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.AddFilm(createFilmDto);

            // Assert
            result.Value.Should().Be(filmDto);
        }

        [Fact]
        public async Task UpdateFilm_ReturnsUpdatedFilm()
        {
            // Arrange
            var filmId = 1;
            var updateFilmDto = new UpdateFilmDto { Title = "Updated Film" };
            var filmDto = new FilmDto { Id = filmId, Title = "Updated Film" };
            A.CallTo(() => _filmService.UpdateFilm(filmId, updateFilmDto)).Returns(filmDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.UpdateFilm(filmId, updateFilmDto);

            // Assert
            result.Value.Should().Be(filmDto);
        }

        [Fact]
        public async Task UpdateFilm_ReturnsNotFound()
        {
            // Arrange
            var filmId = 1;
            var updateFilmDto = new UpdateFilmDto { Title = "Updated Film" };
            A.CallTo(() => _filmService.UpdateFilm(filmId, updateFilmDto)).Returns(Task.FromResult<FilmDto>(null));

            // Act
            var result = await _controller.UpdateFilm(filmId, updateFilmDto);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteFilm_ReturnsDeletedFilm()
        {
            // Arrange
            var filmId = 1;
            var filmDto = new FilmDto { Id = filmId, Title = "Film to Delete" };
            A.CallTo(() => _filmService.DeleteFilm(filmId)).Returns(filmDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.DeleteFilm(filmId);

            // Assert
            result.Value.Should().Be(filmDto);
        }

        [Fact]
        public async Task DeleteFilm_ReturnsNotFound()
        {
            // Arrange
            var filmId = 1;
            A.CallTo(() => _filmService.DeleteFilm(filmId)).Returns(Task.FromResult<FilmDto>(null));

            // Act
            var result = await _controller.DeleteFilm(filmId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UploadImage_ReturnsImageUrl()
        {
            // Arrange
            var filmId = 1;
            var file = A.Fake<IFormFile>();
            var imageUrl = "http://example.com/image.jpg";
            A.CallTo(() => _filmService.UploadImage(file, filmId)).Returns(imageUrl);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.UploadImage(file, filmId);

            // Assert
            result.Value.Should().Be(imageUrl);
        }
    }
}
