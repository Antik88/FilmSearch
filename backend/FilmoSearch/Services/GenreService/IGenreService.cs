using FilmoSearch.DTOs.GenreDtos;

namespace FilmoSearch.Services.GenreService
{
    public interface IGenreService
    {
        Task<CreateGenreDto> AddGenre(CreateGenreDto createGenreDto);
        Task<GenreDto> DeleteGenre(int id);
        Task<List<GenreDto>> GetAllGenres();
        Task<GenreDto> GetSingleGenre(int id);
        Task<UpdateGenreDto> UpdateGenre(int id, UpdateGenreDto GenreDto);
    }
}
