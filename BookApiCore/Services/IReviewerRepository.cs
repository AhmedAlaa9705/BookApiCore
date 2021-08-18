using BookApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Services
{
    public interface IReviewerRepository
    {
        IEnumerable<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        IEnumerable<Review> GetReviewsByReviewer(int reviewerId);
        Reviewer GetReviewerOfReview(int reviewId);
        bool ReviewerExisit(int reviewerId);
        void Insert(Reviewer reviewer);
        void Update(Reviewer reviewer);
        void Delete(Reviewer Revierewer);
        void Save();
    }
}
