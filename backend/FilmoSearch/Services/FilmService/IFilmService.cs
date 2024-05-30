using FilmoSearch.DTOs.Film;
using FilmoSearch.DTOs.FilmDtos;

namespace FilmoSearch.Services.FilmService
{
    public interface IFilmService
    {
        public Task<FilmDto> AddFilm(CreateFilmDto filmCreateDto);

        public Task<FilmDto?> DeleteFilm(int filmId);

        public Task<List<AllFilmsDto>> GetAllFilms(string? title,
            int? releaseYear, IEnumerable<string>? genreNames);

        public Task<FilmDto?> GetSingleFilm(int filmId);

        public Task<FilmDto?> UpdateFilm(int filmId, UpdateFilmDto updateFilmDto);

        Task<string> UploadImage(IFormFile formFile, int actorId);
    }
}
