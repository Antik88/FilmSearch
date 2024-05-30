using FilmoSearch.DTOs.ActorDtos;
using FilmoSearch.DTOs.GenreDtos;
using FilmoSearch.DTOs.ReviewDtos;

namespace FilmoSearch.DTOs.Film
{
    public class FilmDto
    {
        public int Id { get; set; }
        
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        public int? Duration { get; set; }

        public string? Director { get; set; }

        public string ImageName { get; set; }

        public List<GenreDto>? Genres { get; set; }
        public List<ActorDto>? Actors { get; set; }
        public List<ShortReviewDto>? Reviews { get; set; }
    }
}
