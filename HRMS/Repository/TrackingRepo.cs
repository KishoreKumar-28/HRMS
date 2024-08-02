using HRMS.Data;
using HRMS.Models;
using System.Security.Claims;

namespace HRMS.Repository
{
    // Implementation of tracking interface
    public class TrackingRepo : ITrackingRepo
    {
        private readonly HrmsdbContext _Context;

        public TrackingRepo(HrmsdbContext Context)
        {
            _Context = Context;
        }
        // Get all the tracking trainings
        public IEnumerable<TrackingTraining> GetAll()
        {
            return _Context.TrackingTrainings.ToList();
        }
        // Get the details by employee id.
        public TrackingTraining GetByEmpId(int id)
        {
            return _Context.TrackingTrainings.FirstOrDefault(tt => tt.EmployeeId == id);
        }
        // Enroll the name for trainings
        public void Add(TrackingTraining track)
        {
            // Check employee can only register for different trainings
            bool recordexists=_Context.TrackingTrainings.Any(t=>t.TrainingId== track.TrainingId);
            // If same trainings registered.
            if(recordexists)
            {
                throw new CustomException("You have already registered for this training");
            }
            TrackingTraining tracking = new TrackingTraining();
            tracking.TrackingId = track.TrackingId;
            tracking.TrainingId = track.TrainingId;
            tracking.EmployeeId = track.EmployeeId;
            tracking.IsRegister= true;
            _Context.TrackingTrainings.Add(tracking);
            _Context.SaveChanges();
        }
        // Update the attendance for employee
        public void Update(TrackingTraining track)
        { 
            TrackingTraining tracking = _Context.TrackingTrainings.FirstOrDefault(t => t.TrackingId == track.TrackingId);
            if (tracking != null)
            {
                // Update the properties of the existing trackingtraining details
                tracking.EmployeeId = track.EmployeeId;
                tracking.TrainingId = track.TrainingId;
                tracking.IsRegister = track.IsRegister;
                _Context.SaveChanges();
            }
        }
    }
}
