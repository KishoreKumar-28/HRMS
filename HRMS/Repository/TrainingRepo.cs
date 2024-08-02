using HRMS.Data;
using HRMS.Models;

namespace HRMS.Repository
{
    // Implementation of training interface
    public class TrainingRepo : ITrainingRepo
    {
        private readonly HrmsdbContext _context;
        public TrainingRepo(HrmsdbContext context)
        {
            _context = context;
        }
        // Get all training details
        public IEnumerable<Training> GetAllTrainingDetails(int id)
        {
            return _context.Training.ToList();
        }
        // Get only upcoming trainings
        public IEnumerable<Training> GetAllUpcomingTrainings()
        {
            DateTime currentDate = DateTime.Now;
            var upcomingTrainings = _context.Training.Where(t => t.StartDate > currentDate).ToList();
            return upcomingTrainings;
        }
        // Get training details by dept id.
        public Training GetByDeptId(int id)
        {
            var train = _context.Training.FirstOrDefault(training => training.DepartmentId == id);
            if (train == null)
            {
                throw new Exception("Training not found for this department...");
            }
            return train;
        }
        // Get training details by training id.
        public Training GetByTrainingId(int id)
        {
            var train = _context.Training.FirstOrDefault(training => training.TrainingId == id);
            if (train == null)
            {
                throw new Exception("Training not found...");
            }
            return train;               
        }
        // Add training details
        public void AddTraining(Training training)
        {
            // Check if given training name is already exists.
            bool recordexists = _context.Training.Any(t => t.TrainingName == training.TrainingName );
            // if exists then throw exception.
            if (recordexists)
            {
                throw new CustomException("This training already exits..");
            }
            // or add the training
            _context.Training.Add(training);
            _context.SaveChanges();
        }
        // Update the training
        public void UpdateTraining(Training updatedTraining)
        {
            // if training id is not null then update.
            Training training = _context.Training.FirstOrDefault(t => t.TrainingId == updatedTraining.TrainingId);
            if (training != null)
            {
                // Update the properties of the existing training 
                training.DepartmentId = updatedTraining.DepartmentId;
                training.TrainingName = updatedTraining.TrainingName;
                training.Trainer = updatedTraining.Trainer;
                training.Location = updatedTraining.Location;
                training.StartDate = updatedTraining.StartDate;
                training.EndDate = updatedTraining.EndDate;
                //training.CreatedBy = updatedTraining.CreatedBy;
                training.UpdatedTime = DateTime.Now;
                training.IsActive = updatedTraining.IsActive;
                _context.SaveChanges();
            }
        }
        // SoftDelete the training
        public void DeleteTraining(int id)
        {
            Training training = GetByTrainingId(id);
            if (training != null)
            {
                training.IsActive = false;
                UpdateTraining(training);
            }
        }    
    }
}
