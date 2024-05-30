namespace FilmoSearch.DTOs.UserDtos
{
    public class CreateUserDto
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}
