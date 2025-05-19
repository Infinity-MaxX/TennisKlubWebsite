using TennisWebsite.ClassLibrary.Interfaces;
using TennisWebsite.ClassLibrary.Models;

namespace TennisWebsite.ClassLibrary.Services
{
    public class GearService : IGearService
    {
        #region Instances
        private List<Gear> _gearRepo;
        #endregion

        #region Properties
        public int Count { get { return _gearRepo.Count(); } }
        public bool Status { get; set; }
        #endregion

        #region Constructor
        public GearService()
        {
            _gearRepo = new List<Gear>();
        }
        #endregion

        #region Methods

        public void AddGear(string name, string? description)
        {
            throw new NotImplementedException();
        }

        public void BookGear(int bookingID, int gearID)
        {
            throw new NotImplementedException();
        }

        public void CheckStatus(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteGear(int id)
        {
            throw new NotImplementedException();
        }

        public List<Gear> GetAll()
        {
            throw new NotImplementedException();
        }

        public Gear GetGear(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
