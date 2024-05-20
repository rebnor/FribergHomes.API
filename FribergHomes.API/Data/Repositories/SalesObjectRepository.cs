using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace FribergHomes.API.Data.Repositories
{
    /* Sales Object Repository talking to ApplicationDBContext and inherit from Interface SalesObject
     * All in Async.
     * @ Author: Rebecka 2024-04-15
     * @ Update: I Included Realtor and Agency in GetSalesObjectByIdAsync, i think its needed  // Rebecka 2023-04-23
     * @ Update: Added inclusion of Realtor, Agency, County and Category when querying DB for SalesObject(s).
     *           Changed GetSalesObjectsByCountyAsync parameter to int countyId (County county) //Tobias 2024-04-23
     * @ Upodate: Added GetCountyByNameAsync() method, because its needed with the DTO&Model-mapping / Reb 2024-05-25
     * @ Update: Added GetSalesObjectsByRealtorAsync method / Tobias 2024-04-29
     * @ Update: Added GetSalesObjectsByCategoryAsync(int categoryId) / Reb 2024-05-02
     */
    public class SalesObjectRepository : ISalesObject
    {
        private readonly ApplicationDBContext _appDBctx;

        public SalesObjectRepository(ApplicationDBContext appDBctx)
        {
            _appDBctx = appDBctx;
        }

        public async Task<SalesObject> AddSalesObjectAsync(SalesObject salesObject)
        {
            await _appDBctx.AddAsync(salesObject);
            await _appDBctx.SaveChangesAsync();
            return salesObject;
        }

        public async Task DeleteSalesObjectAsync(SalesObject salesObject)
        {
            _appDBctx.Remove(salesObject);
            await _appDBctx.SaveChangesAsync();
        }

        public async Task<SalesObject> GetSalesObjectByIdAsync(int? id)
        {
            var salesObject = await _appDBctx.SalesObjects
                .Include(s=>s.Realtor).ThenInclude(r=>r.Agency)
                .Include(x => x.County)    //\\
                .Include(x => x.Category) // Tobias 2024-04-23
                .FirstOrDefaultAsync(s => s.Id == id);
            return salesObject;
        }

        public async Task<List<SalesObject>> GetAllSalesObjectsAsync()
        {
            var salesObjects = await _appDBctx.SalesObjects
                .Include(x => x.Realtor).ThenInclude(y => y.Agency) //\\
                .Include(x => x.County)                            //  \\
                .Include(x => x.Category)                         // Tobias 2024-04-23
                .ToListAsync();
            return salesObjects;
        }

        public async Task<List<SalesObject>> GetSalesObjectsByCountyAsync(int id)
        {
            var salesObjects = await _appDBctx.SalesObjects
                .Where(s => s.County.Id == id)
                .Include(x => x.Realtor).ThenInclude(y => y.Agency) //\\
                .Include(x => x.County)                            //  \\
                .Include(x => x.Category)                         // Tobias 2024-04-23
                .ToListAsync();
            return salesObjects;
        }

        public async Task<List<SalesObject>> GetSalesObjectsByRealtorAsync(string id)          
        {
            var salesObjects = await _appDBctx.SalesObjects
                .Where(s => s.Realtor.Id == id)
                .Include(x => x.Realtor).ThenInclude(y => y.Agency) //\\
                .Include(x => x.County)                            //  \\
                .Include(x => x.Category)                         // Tobias 2024-04-23
                .ToListAsync();
            return salesObjects;
        }

        public async Task<SalesObject> UpdateSalesObjectAsync(SalesObject salesObject)
        {
            _appDBctx.Update(salesObject);
            await _appDBctx.SaveChangesAsync();
            return salesObject;
        }

        // Update: Added this because its needed in the ModelMapper / Reb 2024-04-25
        public async Task<County> GetCountyByNameAsync(string countyName) 
        {
            var county = await _appDBctx.Counties.FirstOrDefaultAsync(c=>c.Name.ToLower() == countyName.ToLower());
            return county;
        }
        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            var category = await _appDBctx.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            return category;
        }
        public async Task<List<SalesObject>> GetSalesObjectsByCountyNameAsync(string countyName)
        {
            var salesObject = await _appDBctx.SalesObjects.Where(s => s.County.Name.ToLower() == countyName.ToLower()).ToListAsync();
            return salesObject;
        }

        public async Task<List<SalesObject>> GetRealtorsSalesObjectsAsync(string realtorId)     //TODO: Ta bort dublett
        {
            var salesObjects = await _appDBctx.SalesObjects.Where(s=>s.Realtor.Id == realtorId).ToListAsync();
            return salesObjects;

        }

        public async Task<List<SalesObject>> GetSalesObjectsByCategoryAsync(int categoryId)
        {
            var salesObjects = await _appDBctx.SalesObjects.Where(s => s.Category.Id == categoryId).Include(s=>s.County).Include(s=>s.Realtor).ThenInclude(r=>r.Agency).ToListAsync();
            return salesObjects;

        }

    }
}
