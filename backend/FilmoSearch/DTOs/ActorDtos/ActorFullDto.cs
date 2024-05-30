using FilmoSearch.DTOs.FilmDtos;

namespace FilmoSearch.DTOs.ActorDtos
{
    public class ActorFullDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string ImageName { get; set; }

        public List<ShortFilmDto> Films { get; set; }
    }
}
