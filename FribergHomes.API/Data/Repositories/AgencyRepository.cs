using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        public async Task<Agency> GetAgencyByRealtorEmail(string email)
        {
            //var realtor = await _dbContext.Realtors.FirstOrDefaultAsync(r => r.Email.ToLower() == email.ToLower());
            //var agency = await _dbContext.Agencies.FirstOrDefaultAsync(a=>a.Id == realtor.Agency.Id);
            //return agency;
            // Konvertera e-postadressen till gemener för att undvika problem med gemener/stora bokstäver
            string normalizedEmail = email.ToLower();

            // Hämta mäklaren baserat på den normaliserade e-postadressen
            var realtor = await _dbContext.Realtors
                .Include(r => r.Agency) // Inkludera byrån för mäklaren
                .FirstOrDefaultAsync(r => r.Email.ToLower() == normalizedEmail);

            // Kontrollera om mäklaren hittades
            if (realtor != null)
            {
                // Returnera byrån som mäklaren tillhör
                return realtor.Agency;
            }
            else
            {
                // Om mäklaren inte hittades, returnera null eller kasta ett undantag
                return null; // Eller kasta ett undantag: throw new Exception("Mäklaren hittades inte med den angivna e-postadressen.");
            }
        }

        public async Task<List<SalesObject>> GetSalesObjectsAtAgencyAsync(int id)
        {
            return await _dbContext.SalesObjects.Where(s=>s.Realtor.Agency.Id == id).Include(s=>s.County).Include(s=>s.Realtor).ToListAsync();

        }



    }
}
