using System.Runtime.CompilerServices;

namespace FilmoSearch.DTOs.ActorDtos
{
    public class ActorDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? ImageName { get; set; }
    }
}
