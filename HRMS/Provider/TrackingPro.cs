using HRMS.Models;
using HRMS.Repository;

namespace HRMS.Provider
{
    // Implementation of tracking interface
    public class TrackingPro : ITrackingPro
    {
        private readonly ITrackingRepo _repo;
        public TrackingPro(ITrackingRepo repo)
        {
            _repo = repo;
        }
        // Get all the tracking trainings
        IEnumerable<TrackingTraining> ITrackingPro.GetAll()
        {
            return _repo.GetAll();
        }
        // Get the details by employee id.
        public TrackingTraining GetByEmpId(int id)
        {
            return _repo.GetByEmpId(id);
        }
        // Enroll the name for trainings
        public void Add(TrackingTraining track)
        {
            _repo.Add(track);
        }
        // Update the attendance for employee
        public void Update(TrackingTraining track)
        {
            _repo.Update(track);
        }
    }
}
