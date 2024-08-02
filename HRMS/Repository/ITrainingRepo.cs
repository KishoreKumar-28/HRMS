using HRMS.Models;

namespace HRMS.Repository
{
    // Training repository interface
    public interface ITrainingRepo
    {
        IEnumerable<Training> GetAllTrainingDetails(int id);
        IEnumerable<Training> GetAllUpcomingTrainings();
        public Training GetByTrainingId(int id);
        public Training GetByDeptId(int id);
        public void AddTraining(Training training);
        public void UpdateTraining(Training updatedTraining);
        public void DeleteTraining(int id);    
    }
}
