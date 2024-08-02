using HRMS.Models;

namespace HRMS.Provider
{
    public interface ITrainingPro
    {
        IEnumerable<Training> GetAllTrainingDetails(int id);
        IEnumerable<Training> GetAllUpcomingTrainings();
        public Training GetByTrainingId(int id);
        public Training GetByDeptId(int id);
        public void AddTraining(Training review);
        public void UpdateTraining(Training updatedReview);
        public void DeleteTraining(int id);
    }
}
