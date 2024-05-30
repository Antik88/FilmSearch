using FakeItEasy;
using FilmoSearch.Controllers;
using FilmoSearch.DTOs.ReviewDtos;
using FilmoSearch.Helpers;
using FilmoSearch.Services.ReviewService;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmoSearch.Tests.Controllers
{
    public class ReviewControllerTests
    {
        private readonly IReviewService _reviewService;
        private readonly ReviewController _controller;

        public ReviewControllerTests()
        {
            _reviewService = A.Fake<IReviewService>();
            _controller = new ReviewController(_reviewService);
        }

        [Fact]
        public async Task GetAllReviews_ReturnsOk()
        {
            // Arrange
            var reviewList = new List<ReviewDto> { new ReviewDto(), new ReviewDto() };
            A.CallTo(() => _reviewService.GetAllReviews()).Returns(reviewList);

            // Act
            var result = await _controller.GetAllReviews();

            // Assert
            result.Value.Should().BeEquivalentTo(reviewList);
        }

        [Fact]
        public async Task GetReviewById_ReturnsOk()
        {
            // Arrange
            var reviewId = 1;
            var reviewDto = new ReviewDto();
            A.CallTo(() => _reviewService.GetReviewById(reviewId)).Returns(reviewDto);

            // Act
            var result = await _controller.GetReviewById(reviewId);

            // Assert
            result.Value.Should().Be(reviewDto);
        }

        [Fact]
        public async Task GetReviewById_ReturnsNotFound()
        {
            // Arrange
            var reviewId = 1;
            A.CallTo(() => _reviewService.GetReviewById(reviewId)).Returns<ReviewDto>(null);

            // Act
            var result = await _controller.GetReviewById(reviewId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AddReview_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var filmId = 1;
            var reviewDto = new CreateReviewDto();
            var expectedReviewDto = new ReviewDto();
            A.CallTo(() => _reviewService.AddReview(filmId, reviewDto)).Returns(expectedReviewDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.AddReview(filmId, reviewDto);

            // Assert
            result.Value.Should().Be(expectedReviewDto);
        }

        [Fact]
        public async Task AddReview_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var filmId = 1;
            var reviewDto = new CreateReviewDto();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.AddReview(filmId, reviewDto);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateReview_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var reviewId = 1;
            var reviewDto = new UpdateReviewDto();
            var expectedReviewDto = new ReviewDto();
            A.CallTo(() => _reviewService.UpdateReview(reviewId, reviewDto)).Returns(expectedReviewDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.UpdateReview(reviewId, reviewDto);

            // Assert
            result.Value.Should().Be(expectedReviewDto);
        }

        [Fact]
        public async Task UpdateReview_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var reviewId = 1;
            var reviewDto = new UpdateReviewDto();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.UpdateReview(reviewId, reviewDto);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteReview_WithAuthorization_ReturnsOk()
        {
            // Arrange
            var reviewId = 1;
            var expectedReviewDto = new ReviewDto();
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).Returns(expectedReviewDto);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.DeleteReview(reviewId);

            // Assert
            result.Value.Should().Be(expectedReviewDto);
        }

        [Fact]
        public async Task DeleteReview_WithoutAuthorization_ReturnsForbidden()
        {
            // Arrange
            var reviewId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.DeleteReview(reviewId);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteReview_ReturnsNotFound()
        {
            // Arrange
            var reviewId = 1;
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).Returns<ReviewDto>(null);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, Consts.adminRole) }))
            };

            // Act
            var result = await _controller.DeleteReview(reviewId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
