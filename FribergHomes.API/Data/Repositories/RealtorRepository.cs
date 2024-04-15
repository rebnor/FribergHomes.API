using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using System.Reflection.Metadata.Ecma335;

namespace FribergHomes.API.Data.Repositories
{
    //Author: Sanna 
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

        public async Task DeleteRealtorAsync(int id)
        {
            var realtor = await _appDbCtx.Realtors.FirstOrDefaultAsync(r => r.Id == id);
            _appDbCtx.Realtors.Remove(realtor);           
            return await _appDbCtx.SaveChangesAsync();
        }

        public async Task<List<Realtor>> GetAllRealtorsAsync()
        {
            var realtors = await _appDbCtx.Realtors.OrderBy(r => r.LastName).ThenBy(r => r.FirstName).ToListAsync();
            return realtors; 
        }

        public async Task<List<Realtor>> GetRealtorsByAgencyAsync(Agency agency)
        {
            return await _appDbCtx.Realtors.Where(r => r.Agency == agency).ToListAsync();            
        }

        public async Task<Realtor> GetRealtorByIdAsync(int id)
        {
            var realtor = await _appDbCtx.Realtors.FirstOrDefault(r => r.Id == id);
            return realtor; 
        }

        public async Task<Realtor> UpdateRealtorAsync(Realtor realtor)
        {
            await _appDbCtx.Realtors.Update(realtor);
            await _appDbCtx.SaveChangesAsync();
            return realtor; 
        }
    }
}
