using HRMS.Models;
using HRMS.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace HRMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PerformanceReviewController : ControllerBase
    {

        private readonly IReviewProvider _reviewProvider;

        public PerformanceReviewController(IReviewProvider reviewProvider)
        {
            _reviewProvider = reviewProvider;
        }
        

        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        [HttpGet]
        public IEnumerable<PerformanceReview> GetPerformanceReviewsByhr()
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var review = _reviewProvider.GetPerformanceReviewsByhr(id);
            
            return review;
        }
        [HttpGet]
        public IEnumerable<PerformanceReview> GetPerformanceReviewsBydepartmenthead()
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var review = _reviewProvider.GetPerformanceReviewsBydepartmenthead(id);

            return review;
        }
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head,Employee")]
        [HttpGet]
        public ActionResult<PerformanceReview> GetReviewById(int id)
        {
            try
            {
                var review = _reviewProvider.GetReviewById(id);
                if (review == null)
                {
                    return NotFound();
                }
                return review;
            }
         catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head,Employee")]
        [HttpGet]
        public ActionResult<PerformanceReview> GeByemployeeId()
        { 
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var review = _reviewProvider.GetByEmployeeId(id);
            if (review == null)
            {
                return NotFound();
            }
            return review;
        }
        [Authorize(Roles = "HR Admin,Department Head")]
        [HttpPost]
        public ActionResult AddReview(PerformanceReview review)
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            {
                _reviewProvider.AddReview(review,id);
                return Ok();
            }
        }
        [Authorize(Roles = "HR Admin,Department Head")]
        [HttpPut]
        public ActionResult UpdateReview(int id, PerformanceReview updatedReview)
        {
            var existingReview = _reviewProvider.GetReviewById(id);
            if (existingReview == null)
            {
                return NotFound();
            }
            updatedReview.ReviewId = id;
            _reviewProvider.UpdateReview(updatedReview);
            return Ok();
        }
        [Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head,Employee")]
        [HttpDelete]
        public ActionResult DeleteReview(int id)
        {
            var existingReview = _reviewProvider.GetReviewById(id);
            if (existingReview == null)
            {
                return NotFound();
            }
            _reviewProvider.DeleteReview(id);
            return Ok();
        }

    }
}

