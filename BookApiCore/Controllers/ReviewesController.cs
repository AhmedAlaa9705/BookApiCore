using BookApiCore.Dtos;
using BookApiCore.Models;
using BookApiCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewesController : Controller
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IBookRepository bookRepository;
        private readonly IReviewerRepository reviewerRepository;
        public ReviewesController(IReviewRepository _reviewRepository, IBookRepository _bookRepository, IReviewerRepository _reviewerRepository)
        {
            reviewRepository = _reviewRepository;
            bookRepository = _bookRepository;
            reviewerRepository = _reviewerRepository;
        }
        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = reviewRepository.GetReviews();
            if (!ModelState.IsValid)
                return BadRequest();
            List<ReviewDto> list = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                var ReviewDto = new ReviewDto
                {

                    Id = review.Id,
                    Headine = review.Headine,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                };
                list.Add(ReviewDto);

            }
            return Ok(list);
        }
        [HttpGet("{reviewId}", Name = "GetReview")]
        public IActionResult GetReview(int reviewId)
        {
            if (!reviewRepository.ReviewExisit(reviewId))
                return NotFound();
            var review = reviewRepository.GetReview(reviewId);
            var ReviewDto = new ReviewDto
            {
                Id = review.Id,
                Headine = review.Headine,
                ReviewText = review.ReviewText,
                Rating = review.Rating
            };
            return Ok(ReviewDto);


        }
        [HttpGet("books/{bookId}")]
        public IActionResult GetReviewsOfAbook(int bookId)
        {
            if (!bookRepository.BookExisit(bookId))
                return NotFound();
            var reviews = reviewRepository.GetReviewsOfAbook(bookId);
            if (!ModelState.IsValid)
                return BadRequest();
            List<ReviewDto> list = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                var ReviewDto = new ReviewDto
                {
                    Id = review.Id,
                    Headine = review.Headine,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                };
                list.Add(ReviewDto);
            }
            return Ok(list);

        }
        [HttpGet("{reviewId}/books")]
        public IActionResult GetBookOfAReview(int reviewId)
        {
            if (!reviewRepository.ReviewExisit(reviewId))
                return NotFound();
            var book = reviewRepository.GetBookofAReview(reviewId);
            if (!ModelState.IsValid)
                return BadRequest();
            var bookDto = new BookDto
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                DatePublished = book.DatePublished
            };
            return Ok(bookDto);

        }
        [HttpPost]
        public IActionResult Create([FromBody] Review review)
        {


            if (!reviewerRepository.ReviewerExisit(review.Reviewer.Id))
                ModelState.AddModelError("", "Reviewer Does not exist");

            if (!reviewerRepository.ReviewerExisit(review.Book.Id))
                ModelState.AddModelError("", "Book Does not exist");

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            review.Book = bookRepository.GetBook(review.Book.Id);
            review.Reviewer = reviewerRepository.GetReviewer(review.Reviewer.Id);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Somthing went wrong saving the review");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();

            reviewRepository.Insert(review);
            reviewRepository.Save();
            return CreatedAtRoute("GetReview", new { reviewId = review.Id }, review);


        }
        [HttpPut("{reviewId}")]
        public IActionResult Update(int reviewId, [FromBody] Review review)
        {
            if (review == null)
                return BadRequest();

            if (!reviewRepository.ReviewExisit(reviewId))
                return NotFound();

            if (!reviewerRepository.ReviewerExisit(review.Reviewer.Id))
                ModelState.AddModelError("", "Reviewer Does not exist");

            if (!reviewerRepository.ReviewerExisit(review.Book.Id))
                ModelState.AddModelError("", "Book Does not exist");

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            review.Book = bookRepository.GetBook(review.Book.Id);
            review.Reviewer = reviewerRepository.GetReviewer(review.Reviewer.Id);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Somthing went wrong saving the review");
                return StatusCode(500, ModelState);
            }
            reviewRepository.Update(review);
            reviewRepository.Save();
            return NoContent();


        }
        [HttpDelete("{reviewId}")]
        public IActionResult Delete(int reviewId)
        {
            if (!reviewRepository.ReviewExisit(reviewId))
                return NotFound();
        
            if (ModelState.IsValid)
                reviewRepository.Delete(reviewId);
            reviewRepository.Save();
            return NoContent();
        
        }
    }
}
