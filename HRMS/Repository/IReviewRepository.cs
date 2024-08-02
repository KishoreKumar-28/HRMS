using HRMS.Models;

namespace HRMS.Repository
{
    public interface IReviewRepository
    {
       
        PerformanceReview GetReviewById(int id);
        public PerformanceReview AddReview(PerformanceReview review,int id);
        public PerformanceReview UpdateReview(PerformanceReview updatedReview);
        IEnumerable<PerformanceReview> GetPerformanceReviewsBydepartmenthead(int hrid);
        IEnumerable<PerformanceReview> GetPerformanceReviewsByhr(int hrid);
        PerformanceReview GetByEmployeeId(int id);
        public PerformanceReview DeleteReview(int id);
    }
}
