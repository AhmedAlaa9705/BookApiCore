using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookDbContext db;
        public ReviewRepository(BookDbContext _db)
        {
            db = _db;
        }

        public void Delete(int reviewID)
        {
            Review review = db.Reviews.Find(reviewID);
            db.Remove(review);
        }

        public void DeleteReviews(List<Review> reviews)
        {
            db.RemoveRange(reviews);
        }

        public Book GetBookofAReview(int reviewId)
        {
            var bookId= db.Reviews.Where(a => a.Id == reviewId).Select(b => b.Book.Id).SingleOrDefault();
            return db.Books.Where(a => a.Id == bookId).SingleOrDefault();
        }

        public Review GetReview(int id)
        {
            return db.Reviews.Where(a => a.Id == id).SingleOrDefault();
        }

        public IEnumerable<Review> GetReviews()
        {
            return db.Reviews.ToList();
        }

        public IEnumerable<Review> GetReviewsOfAbook(int bookId)
        {

            return db.Reviews.Where(a => a.Book.Id == bookId).ToList();
            
        }

        public void Insert(Review review)
        {
            db.Add(review);
        }

        public bool ReviewExisit(int reviewId)
        {
            return db.Reviews.Any(a => a.Id==reviewId);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Review review)
        {
            db.Update(review);
        }
    }
}
