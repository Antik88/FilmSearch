using System.Text.Json.Serialization;

namespace FilmoSearch.Models
{
    public class User
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        public List<Review> Reviews { get; set; }
    }
}
