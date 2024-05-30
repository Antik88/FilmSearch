using FilmoSearch.DTOs.GenreDtos;

namespace FilmoSearch.DTOs.FilmDtos
{
    public class AllFilmsDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateOnly? ReleaseDate { get; set; }
        
        public string ImageName { get; set; }

        public List<GenreDto> Genres { get; set; }
    }
}
