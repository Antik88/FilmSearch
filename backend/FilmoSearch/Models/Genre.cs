using System.Text.Json.Serialization;

namespace FilmoSearch.Models
{
    public class Genre
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        [JsonIgnore]
        public List<Film> Films { get; set; }
    }
}
