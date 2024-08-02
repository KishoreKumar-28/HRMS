using HRMS.Models;

namespace HRMS.Repository
{
    // Tracking Training repository interface
    public interface ITrackingRepo
    {
        IEnumerable<TrackingTraining> GetAll();
        public TrackingTraining GetByEmpId(int id);
        public void Add(TrackingTraining track);
        public void Update(TrackingTraining track);
    }
}

