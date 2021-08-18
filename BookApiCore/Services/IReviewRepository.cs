using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviews();
        Review GetReview(int id);
        IEnumerable<Review> GetReviewsOfAbook(int bookId);
        Book GetBookofAReview(int reviewId);
        bool ReviewExisit(int reviewId);
        void Insert(Review review);
        void Update(Review review);
        void Delete(int reviewId);
        void DeleteReviews(List<Review> reviews);
        void Save();
    }
}
