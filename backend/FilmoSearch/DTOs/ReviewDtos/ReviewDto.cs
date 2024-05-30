namespace FilmoSearch.DTOs.ReviewDtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Stars { get; set; }
        public int FilmId { get; set; }
        public string? FilmTitle { get; set; }
        public string UserName { get; set; }
    }
}
