using FilmoSearch.DTOs.ReviewDtos;
using FilmoSearch.Helpers;
using FilmoSearch.Services.ReviewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ReviewDto>>> GetAllReviews()
        {
            return await _reviewService.GetAllReviews();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewById(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        [HttpPost("create"), Authorize]
        public async Task<ActionResult<ReviewDto>> AddReview(
            [FromQuery] int filmId,
            [FromBody] CreateReviewDto createReviewDto)
        {
            return await _reviewService.AddReview(filmId, createReviewDto);
        }

        [HttpPut("update"), Authorize]
        public async Task<ActionResult<ReviewDto>> UpdateReview(
            [FromQuery] int id, 
            [FromBody] UpdateReviewDto updateReviewDto)
        {
            var updatedReview = await _reviewService.UpdateReview(id, updateReviewDto);

            if (updatedReview == null)
                return NotFound();

            return updatedReview;
        }

        [HttpDelete("delete"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<ReviewDto>> DeleteReview(
            [FromQuery] int id)
        {
            var deletedReview = await _reviewService.DeleteReview(id);

            if (deletedReview == null)
            {
                return NotFound();
            }

            return deletedReview;
        }
    }
}
