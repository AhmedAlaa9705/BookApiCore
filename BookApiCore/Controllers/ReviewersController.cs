using BookApiCore.Dtos;
using BookApiCore.Models;
using BookApiCore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewersController : Controller
    {
        private IReviewerRepository reviewerRepository;
        private IReviewRepository revieweRepository;
        public ReviewersController(IReviewerRepository _reviewerRepository, IReviewRepository _reviewRepository)
        {
            reviewerRepository = _reviewerRepository;
            revieweRepository = _reviewRepository;
        }

        [HttpGet]
        public IActionResult GetReviewrs()
        {
            var reviewers = reviewerRepository.GetReviewers();
            if (!ModelState.IsValid)
                return BadRequest();
            List<ReviewerDto> list = new List<ReviewerDto>();
            foreach (var reviewer in reviewers)
            {
                var reviewDto = new ReviewerDto
                {
                    Id = reviewer.Id,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                };
                list.Add(reviewDto);
            }
            return Ok(list);
        }
        [HttpGet("{reviewrId}", Name = "GetReviewer")]
        public IActionResult GetReviewer(int reviewrId)
        {
            if (!reviewerRepository.ReviewerExisit(reviewrId))
                return NotFound();
            var review = reviewerRepository.GetReviewer(reviewrId);
            if (!ModelState.IsValid)
                return BadRequest();
            var reviewerDto = new ReviewerDto
            {
                Id = review.Id,
                FirstName = review.FirstName,
                LastName = review.LastName
            };

            return Ok(reviewerDto);
        }
        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!reviewerRepository.ReviewerExisit(reviewerId))
                return NotFound();
            var reviews = reviewerRepository.GetReviewsByReviewer(reviewerId);
            List<ReviewDto> list = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                var reviewDto = new ReviewDto
                {
                    Id = review.Id,
                    Headine = review.Headine,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating,
                };
                list.Add(reviewDto);
            }
            return Ok(list);
        }
        [HttpGet("{reviewId}/reviewer")]
        public IActionResult GetReviwerOfReviews(int reviewId)
        {
            if (!revieweRepository.ReviewExisit(reviewId))
                return NotFound();
            var reviewer = reviewerRepository.GetReviewerOfReview(reviewId);
            if (!ModelState.IsValid)
                return BadRequest();
            var ReviewerDto = new ReviewerDto
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };
            return Ok(ReviewerDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Reviewer reviewer)
        {
            if (reviewer == null)
                return BadRequest();

            var matshesRviewer = reviewerRepository.GetReviewers().Where(a => a.FirstName.Trim().ToUpper() == reviewer.FirstName.Trim().ToUpper()).FirstOrDefault();
            if (matshesRviewer != null)
            {
                ModelState.AddModelError("", $"Reviewer Name{reviewer.FirstName}is not Exisit ");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();
           

                reviewerRepository.Insert(reviewer);
            reviewerRepository.Save();


            return CreatedAtRoute("GetReviewer", new { reviewerId = reviewer.Id }, reviewer);




        }
        [HttpPut("{reviewerId}")]
        public IActionResult Update(int reviewerId, [FromBody] Reviewer reviewer)
        {
            if (reviewer == null)
                return BadRequest();

            if (reviewerId != reviewer.Id)
                return StatusCode(500, "Not Matching");

            if (!reviewerRepository.ReviewerExisit(reviewerId))
                return NotFound();


            if (!ModelState.IsValid)
                return BadRequest();

   

                reviewerRepository.Update(reviewer);
            reviewerRepository.Save();


            return NoContent();


        }
        [HttpDelete("{reviwerId}")]
        public IActionResult Delete(int reviwerId)
        {
            if (!reviewerRepository.ReviewerExisit(reviwerId))
                return NotFound();
            var reviewr = reviewerRepository.GetReviewer(reviwerId);
            var reviews = reviewerRepository.GetReviewsByReviewer(reviwerId);

            if (!ModelState.IsValid)
                return BadRequest();

          
                reviewerRepository.Delete(reviewr);
            revieweRepository.DeleteReviews(reviews.ToList());

            reviewerRepository.Save();
            revieweRepository.Save();
            return NoContent();
            
        }
    }
}
