using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly BookDbContext db;
        public ReviewerRepository(BookDbContext _db)
        {
            db = _db;
        }

        public void Delete(Reviewer Reviewer)
        {
            db.Remove(Reviewer);
        }

        public Reviewer GetReviewer(int id)
        {
            return db.Reviewers.Where(a => a.Id == id).SingleOrDefault();
        }

        public Reviewer GetReviewerOfReview(int reviewId)
        {
            var revewerId=  db.Reviews.Where(a => a.Id == reviewId).Select(r => r.Reviewer.Id).SingleOrDefault();
            return db.Reviewers.Where(a => a.Id == revewerId).FirstOrDefault();
        }

        public IEnumerable<Reviewer> GetReviewers()
        {
            return db.Reviewers.OrderBy(a=>a.Id).ToList();
        }

        public IEnumerable<Review> GetReviewsByReviewer(int reviewerId)
        {
            return db.Reviews.Where(a=>a.Reviewer.Id==reviewerId).ToList();
        }

        public void Insert(Reviewer reviewer)
        {
            db.Add(reviewer);
        }

        public bool ReviewerExisit(int reviewerId)
        {
            return db.Reviewers.Any(a => a.Id == reviewerId);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Reviewer reviewer)
        {
            db.Update(reviewer);
        }
    }
}
