using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomes.API.Data.Repositories
{
    /* Repository that retrieves and manages Agency objects.
     * Author: Tobias 2024-04-15
     * Update: Added GetRealtorsAtAgencyAsync(int id) / Reb 2024-05-02
     */

    public class AgencyRepository : IAgency
    {
        private readonly ApplicationDBContext _dbContext;

        public AgencyRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Searches for an Agency-object based on Id.
        // If no matches, returns null.
        public async Task<Agency?> GetAgencyByIdAsync(int id)
        {
            var agency = await _dbContext.FindAsync<Agency>(id);
            return agency;
        }

        // Returns all stored Agencies in a List<Agency>.
        // If no Agencies are stored, returns an empty List<Agency>.
        public async Task<List<Agency>> GetAllAgenciesAsync()
        {
            var agencies = await _dbContext.Agencies.ToListAsync();
            return agencies;
        }

        // Takes in a Agency-object and adds it to the DB.
        public async Task AddAgencyAsync(Agency agency)
        {
            await _dbContext.Agencies.AddAsync(agency);
            await _dbContext.SaveChangesAsync();
        }

        // Takes in a Agency-object and updates the object in the DB.
        // Returns the updated Agency object.
        public async Task<Agency> UpdateAgencyAsync(Agency agency)
        {
            _dbContext.Agencies.Update(agency);
            await _dbContext.SaveChangesAsync();
            return agency;
        }

        // Searches for an Agency-object based on Id.
        // If search returns an Agency-object, the object is removed from the DB.
        public async Task DeleteAgencyAsync(int id)
        {
            var agency = await _dbContext.FindAsync<Agency>(id);
            if (agency != null)
            {
                _dbContext.Agencies.Remove(agency);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Realtor>> GetRealtorsAtAgencyAsync(int id)
        {
            var agency = await _dbContext.Agencies.FirstOrDefaultAsync(a=>a.Id == id);
            var realtors = await _dbContext.Realtors.Where(r=>r.Agency.Id == agency.Id).ToListAsync();
            return realtors;
        }


    }
}
