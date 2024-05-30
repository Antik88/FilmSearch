namespace FilmoSearch.DTOs.ReviewDtos
{
    public class ShortReviewDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Stars { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
