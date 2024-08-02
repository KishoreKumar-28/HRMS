using HRMS.Data;
using HRMS.Models;
using System.Security.Claims;

namespace HRMS.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly HrmsdbContext _context;

        public ReviewRepository(HrmsdbContext context)
        {
            _context = context;
        }


       
        public PerformanceReview GetReviewById(int id)

        {

            var rev = _context.PerformanceReviews.FirstOrDefault(r => r.ReviewId == id);

            if (rev == null)

            {

                throw new Exception("Review Id not found");

            }

            return rev;

        }

        public PerformanceReview GetByEmployeeId(int id)
        {
            return _context.PerformanceReviews.FirstOrDefault(r => r.EmployeeId == id && r.IsActive == true);
        }
        public IEnumerable<PerformanceReview> GetPerformanceReviewsBydepartmenthead(int deptheadid)
        {
            Employee employee = _context.Employees.FirstOrDefault(r => r.EmployeeId == deptheadid);
            return _context.PerformanceReviews.Where(r => r.CreatedBy == employee.FirstName);
        }

        public IEnumerable<PerformanceReview> GetPerformanceReviewsByhr(int hrid)
        {
            Employee employee = _context.Employees.FirstOrDefault(r => r.EmployeeId == hrid);
            return _context.PerformanceReviews.Where(r => r.CreatedBy == employee.FirstName);
        }
        public PerformanceReview AddReview(PerformanceReview review,int id)
         
        {
            review.CreatedTime = DateTime.Now;
            review.IsActive = true;
            
            PerformanceReview pro = GetByEmployeeId(review.EmployeeId);
            if (pro != null)
            {
                pro.IsActive = false;
                UpdateReview(pro);

            }
            _context.PerformanceReviews.Add(review);
            _context.SaveChanges();
            return review;
        }


        public PerformanceReview UpdateReview(PerformanceReview updatedReview)
        {
            var review = _context.PerformanceReviews.FirstOrDefault(r => r.ReviewId == updatedReview.ReviewId);
            if (review != null)
            {
                review.EmployeeId = updatedReview.EmployeeId;
                review.ReviewerId = updatedReview.ReviewerId;
                review.ReviewDate = updatedReview.ReviewDate;
                review.Comments = updatedReview.Comments;
                review.UpdatedTime = DateTime.Now;
                review.IsActive = updatedReview.IsActive;
               
                _context.SaveChanges();
            }
            return updatedReview;
        }
        public PerformanceReview DeleteReview(int id)
        {
            PerformanceReview pro = GetByEmployeeId(id);
            if (pro != null)
            {
                pro.IsActive = false;
                UpdateReview(pro);


            }
            return pro;

        }

    }

}

