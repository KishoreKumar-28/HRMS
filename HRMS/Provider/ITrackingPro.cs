using HRMS.Models;

namespace HRMS.Provider
{
    public interface ITrackingPro
    {
        IEnumerable<TrackingTraining> GetAll();
        public TrackingTraining GetByEmpId(int id);
        public void Add(TrackingTraining track);
        public void Update(TrackingTraining track);
    }
}

