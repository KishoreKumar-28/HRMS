using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Provider
{
    public interface IReviewProvider
    {
       
        public PerformanceReview GetReviewById(int id);
        public PerformanceReview GetByEmployeeId(int id);
        IEnumerable<PerformanceReview> GetPerformanceReviewsBydepartmenthead(int hrid);
        IEnumerable<PerformanceReview> GetPerformanceReviewsByhr(int hrid);
        public PerformanceReview AddReview(PerformanceReview review,int id);
        public PerformanceReview UpdateReview(PerformanceReview updatedReview);
        public PerformanceReview DeleteReview(int id);


    }
}
