namespace FilmoSearch.DTOs.Film
{
    public class CreateFilmDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        public int? Duration { get; set; }

        public string? Director { get; set; }

        public List<int>? GenreIds { get; set; }
        public List<int>? ActorIds { get; set; }
    }
}
