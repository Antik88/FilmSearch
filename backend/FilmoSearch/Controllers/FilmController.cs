using FilmoSearch.DTOs.Film;
using Microsoft.AspNetCore.Mvc;
using FilmoSearch.DTOs.FilmDtos;
using FilmoSearch.Services.FilmService;
using Microsoft.AspNetCore.Authorization;
using FilmoSearch.Helpers;

namespace FilmoSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AllFilmsDto>>> GetAllFilms(
            [FromQuery] string? title,
            [FromQuery] int? releaseYear,
            [FromQuery] IEnumerable<string>? genreNames)
        {
            return await _filmService.GetAllFilms(title, releaseYear, genreNames);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmDto>> GetSingleFilm(int id)
        {
            var result = await _filmService.GetSingleFilm(id);

            if (result == null)
            {
                return NotFound($"Film with id: {id} not found");
            }

            return result;
        }

        [HttpPost("create"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<FilmDto>> AddFilm(CreateFilmDto createFilmDto)
        {
            return await _filmService.AddFilm(createFilmDto);
        }

        [HttpPut("update"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<FilmDto?>> UpdateFilm(
            [FromQuery] int id,
            [FromBody] UpdateFilmDto updateFilmDto)
        {
            var result = await _filmService.UpdateFilm(id, updateFilmDto);

            if (result == null)
            {
                return NotFound($"Film with id: {id} not found");
            }

            return result;
        }

        [HttpDelete("delete"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<FilmDto?>> DeleteFilm([FromQuery] int id)
        {
            var result = await _filmService.DeleteFilm(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("uploadImg"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<string>> UploadImage(IFormFile _IFormFile, int filmId)
        {
            return await _filmService.UploadImage(_IFormFile, filmId);
        }
    }
}
