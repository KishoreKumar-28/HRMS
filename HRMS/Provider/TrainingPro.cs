using HRMS.Models;
using HRMS.Repository;

namespace HRMS.Provider
{
    // Implementation of training interface
    public class TrainingPro : ITrainingPro
    {
        private readonly ITrainingRepo _repo;
        public TrainingPro(ITrainingRepo repo)
        {
            _repo = repo;
        }
        // Get all training details
        public IEnumerable<Training> GetAllTrainingDetails(int id)
        {
            return _repo.GetAllTrainingDetails(id);
        }
        // Get details by training id
        public Training GetByTrainingId(int id)
        {
            return _repo.GetByTrainingId(id);
        }
        // Get details by dept id
        public Training GetByDeptId(int id)
        {
            return _repo.GetByDeptId(id);
        }
        // Add training details
        public void AddTraining(Training review)
        {
            _repo.AddTraining(review);
        }
        // Update the training
        public void UpdateTraining(Training updatedReview)
        {
            _repo.UpdateTraining(updatedReview);
        }
        // SoftDelete the training
        public void DeleteTraining(int id)
        {
            Training pro = GetByTrainingId(id);
            if (pro != null)
            {
                pro.IsActive = false;
                UpdateTraining(pro);
            }
        }
        // Get upcoming trainings
        public IEnumerable<Training> GetAllUpcomingTrainings()
        {
            return _repo.GetAllUpcomingTrainings();
        }
    }
}
