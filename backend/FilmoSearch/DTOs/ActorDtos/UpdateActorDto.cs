namespace FilmoSearch.DTOs.ActorDtos
{
    public class UpdateActorDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? BirthDate { get; set; }
    }
}
