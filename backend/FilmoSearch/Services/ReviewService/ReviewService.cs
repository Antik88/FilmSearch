using AutoMapper;
using FilmoSearch.Data;
using FilmoSearch.DTOs.ReviewDtos;
using FilmoSearch.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.ComponentModel;
using System.Security.Claims;

namespace FilmoSearch.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public ReviewService(AppDbContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = dbContext;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<ReviewDto>> GetAllReviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Film)
                .ToListAsync();

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto?> GetReviewById(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Film)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> AddReview(int filmId, CreateReviewDto createReviewDto)
        {
            var film = await _context.Films
                .FirstOrDefaultAsync(f => f.Id == filmId);

            if (film == null)
                return null;

            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Film.Id == filmId && r.User.Id == GetUserId());

            if (existingReview != null)
                return null;

            if (createReviewDto.Stars < 0 || createReviewDto.Stars > 5)
                return null;

            var review = _mapper.Map<Review>(createReviewDto);
            review.Film = film;
            review.User = await _context.Users.SingleAsync(u => u.Id == GetUserId());

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto?> UpdateReview(int id, UpdateReviewDto updatedReviewDto)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null || review.User.Id != GetUserId())
                return null;

            review.Title = updatedReviewDto.Title;
            review.Description = updatedReviewDto.Description;
            review.Stars = updatedReviewDto.Stars;

            await _context.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto?> DeleteReview(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null)
            {
                return null;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReviewDto>(review);
        }
        private int GetUserId()
        {
            return int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
