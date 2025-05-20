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
        public async Task<bool> AddGear(string name, string? description)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> BookGear(int bookingID, int gearID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckStatus(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteGear(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gear>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Gear> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Gear> GetByTypeAsync(string type)
        {
            throw new NotImplementedException();
        }

        public async Task<Gear> GetGear(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
