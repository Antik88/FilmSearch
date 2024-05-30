using System.Text.Json.Serialization;

namespace FilmoSearch.Models
{
    public class Film
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        public int? Duration { get; set; }

        public string? Director { get; set; }

        public string? ImageName { get; set; }

        [JsonIgnore]
        public List<Genre>? Genres { get; set; }

        [JsonIgnore]
        public List<Actor>? Actors { get; set; }

        [JsonIgnore]
        public List<Review>? Reviews { get; set; }
    }
}
