using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomes.API.Data.Repositories
{
    /* County Repository talking to ApplicationDBContext and inherit from Interface County
     * All in Async.
     * @ Author: Rebecka 2024-04-15
     */
    public class CountyRepository : ICounty
    {
        private readonly ApplicationDBContext _appDBctx;

        public CountyRepository(ApplicationDBContext appDBctx) 
        {
            _appDBctx = appDBctx;
        }

        public async Task<County> AddCountyAsync(County county)
        {
            _appDBctx.Counties.Add(county);
            await _appDBctx.SaveChangesAsync();
            return county;
        }

        public async Task DeleteCountyAsync(int id)
        {
            var county = await _appDBctx.Counties.FirstOrDefaultAsync(c => c.Id == id);
            _appDBctx.Counties.Remove(county);
            await _appDBctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<County>> GetAllCountiesAsync()
        {
            var counties = _appDBctx.Counties.OrderBy(c=>c.Name);
            return counties;
        }

        public async Task<County> GetCountyByIdAsync(int id)
        {
            var county = await _appDBctx.Counties.FirstOrDefaultAsync(c => c.Id == id);
            return county;
        }

        public async Task<County> GetCountyByNameAsync(string name)
        {
            var county = await _appDBctx.Counties.FirstOrDefaultAsync(c=>c.Name.ToLower() == name.ToLower());
            return county;
        }

        public async Task<County> UpdateCountyAsync(County county)
        {
            _appDBctx.Counties.Update(county);
            await _appDBctx.SaveChangesAsync();
            return county;
        }
    }
}
