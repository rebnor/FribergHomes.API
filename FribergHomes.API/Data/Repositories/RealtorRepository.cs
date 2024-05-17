using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;

namespace FribergHomes.API.Data.Repositories
{
    //Author: Sanna 
    // @ Update: Included Agency & Salesobjects when you Get realtor/realtors / Reb 2024-04-24
    // @ Update: Added GetAgencyByNameAsync() beasue its needed in ModelMapper / Reb 2024-04-24
    // @ Update: Added GetRealtorsSalesObjects() becuase its needed in ModelMapper / Reb 2024-04-25 <- Kanske inte alls behövs? SE ÖVER
    // @ Update: GetAllRealtorsAsync - removed include(salesobjects). / Tobias 2024-04-29
    public class RealtorRepository : IRealtor
    {
        private readonly ApplicationDBContext _appDbCtx;

        public RealtorRepository(ApplicationDBContext appDbCtx)
        {
            _appDbCtx = appDbCtx;
        }
        public async Task<Realtor> AddRealtorAsync(Realtor realtor)
        {
            _appDbCtx.Realtors.Add(realtor);
            await _appDbCtx.SaveChangesAsync();
            return realtor;
        }

        public async Task DeleteRealtorAsync(Realtor realtor, Realtor newRealtor)
        {
            // test reb 2024-04-30
            realtor.Agency = null; 
            var saleobjects = await _appDbCtx.SalesObjects.Where(s => s.Realtor.Id == realtor.Id).ToListAsync();
            foreach (var so in saleobjects)
            {
                //_ = so.Realtor == newRealtor;
                so.Realtor = newRealtor;
                _appDbCtx.Update(so);
                await _appDbCtx.SaveChangesAsync();
            }
            _appDbCtx.Remove(realtor);
            await _appDbCtx.SaveChangesAsync();

        }

        public async Task<List<Realtor>> GetAllRealtorsAsync()
        {
            var realtors = await _appDbCtx.Realtors.Include(r=>r.Agency).OrderBy(r => r.LastName).ThenBy(r => r.FirstName).ToListAsync();
            return realtors;
           
        }

        public async Task<List<Realtor>> GetRealtorsByAgencyAsync(Agency agency)
        {
            return await _appDbCtx.Realtors.Include(r=>r.Agency).Where(r => r.Agency == agency).ToListAsync();
        }

        public async Task<Realtor> GetRealtorByIdAsync(string id)
        {
            //var realtor = await _appDbCtx.Realtors.FirstOrDefaultAsync(r => r.Id == id);
            var realtor = await _appDbCtx.Realtors.Include(r => r.Agency).FirstOrDefaultAsync(r => r.Id == id);
            return realtor;
        }

        public async Task<Realtor> UpdateRealtorAsync(Realtor realtor)
        {
            _appDbCtx.Realtors.Update(realtor);
            await _appDbCtx.SaveChangesAsync();
            return realtor;
        }

        
        public async Task<Agency> GetAgencyByNameAsync(string name) 
        {
            var agency = await _appDbCtx.Agencies.FirstOrDefaultAsync(a=>a.Name.ToLower() == name.ToLower());
            return agency;
        }
        public async Task<List<SalesObject>> GetRealtorsSalesObjects(Realtor realtor)
        {
            return await _appDbCtx.SalesObjects.Where(s => s.Realtor.Id == realtor.Id).Include(s=>s.County).Include(ss=>ss.Category).ToListAsync();
        }
    }
}
