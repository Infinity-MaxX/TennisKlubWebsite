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
            
        }
        #endregion

        #region Methods
        public Task<bool> AddGear(string name, string? description)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BookGear(int bookingID, int gearID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckStatus(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGear(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gear>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Gear> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Gear> GetByTypeAsync(string type)
        {
            throw new NotImplementedException();
        }

        public Task<Gear> GetGear(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
