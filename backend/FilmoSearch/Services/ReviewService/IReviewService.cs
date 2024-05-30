using FilmoSearch.DTOs.ReviewDtos;
using FilmoSearch.Models;

namespace FilmoSearch.Services.ReviewService
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllReviews();

        Task<ReviewDto?> GetReviewById(int id);

        Task<ReviewDto> AddReview(int filmId, CreateReviewDto createReviewDto);

        Task<ReviewDto?> UpdateReview(int id, UpdateReviewDto updatedReviewDto);
        
        Task<ReviewDto?> DeleteReview(int id);
    }
}
