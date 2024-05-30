using Newtonsoft.Json;

namespace FilmoSearch.Models
{
    public class Actor
    {
        public int Id { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public DateOnly? BirthDate { get; set; }

        public string? ImageName { get; set; }
        
        [JsonIgnore]
        public List<Film>? Films { get; set; }
    }
}
