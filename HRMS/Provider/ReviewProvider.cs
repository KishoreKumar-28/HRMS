using HRMS.Models;
using HRMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace HRMS.Provider
{
    public class ReviewProvider : IReviewProvider
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewProvider(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

       

        public PerformanceReview GetReviewById(int id)
        {
            return _reviewRepository.GetReviewById(id);
        }
        public PerformanceReview GetByEmployeeId(int id)
        {
            return _reviewRepository.GetByEmployeeId(id);
        }
        public IEnumerable<PerformanceReview> GetPerformanceReviewsBydepartmenthead(int hrid)
        {
            return _reviewRepository.GetPerformanceReviewsBydepartmenthead(hrid);
        }
        public IEnumerable<PerformanceReview> GetPerformanceReviewsByhr(int hrid)
        {
            return _reviewRepository.GetPerformanceReviewsByhr(hrid);
        }
        public PerformanceReview AddReview(PerformanceReview review,int id)
        {
            return _reviewRepository.AddReview(review, id);
        }

        public PerformanceReview UpdateReview(PerformanceReview updatedReview)
        {
            return _reviewRepository.UpdateReview(updatedReview);
        }
        public PerformanceReview DeleteReview(int id)
        {
           return  _reviewRepository.DeleteReview(id);
        }


    }
}

