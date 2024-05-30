using FilmoSearch.DTOs.GenreDtos;
using FilmoSearch.Helpers;
using FilmoSearch.Services.GenreService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FilmoSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GenreDto>>> GetAllGenres()
        {
            var result = await _genreService.GetAllGenres();

            Log.Information("Get all genres => {@result}", result);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetSingleGenre(int id)
        {
            var genre = await _genreService.GetSingleGenre(id);
            if (genre == null)
                return NotFound($"Genre with id: {id} not found");

            return genre;
        }

        [HttpPost("create"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<CreateGenreDto>> AddGenre(CreateGenreDto createGenreDto)
        {
            return await _genreService.AddGenre(createGenreDto);
        }

        [HttpPut("update"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<UpdateGenreDto>> UpdateGenre([FromQuery] int id, 
            [FromBody] UpdateGenreDto updateGenreDto)
        {
            var updatedGenre = await _genreService.UpdateGenre(id, updateGenreDto);
            if (updatedGenre == null)
                return NotFound($"Genre with id: {id} not found");

            return updatedGenre;
        }

        [HttpDelete("delete"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<GenreDto>> DeleteGenre([FromQuery] int id)
        {
            var deletedGenre = await _genreService.DeleteGenre(id);
            if (deletedGenre == null)
                return NotFound();

            return deletedGenre;
        }
    }
}
