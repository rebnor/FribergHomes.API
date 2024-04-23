﻿using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomes.API.Data.Repositories
{
    /* Sales Object Repository talking to ApplicationDBContext and inherit from Interface SalesObject
     * All in Async.
     * @ Author: Rebecka 2024-04-15
     * @ Update: I Included Realtor and Agency in GetSalesObjectByIdAsync, i think its needed  // Rebecka 2023-04-23
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
            var salesObject = await _appDBctx.SalesObjects.Include(s=>s.Realtor).ThenInclude(r=>r.Agency).FirstOrDefaultAsync(s => s.Id == id);
            return salesObject;
        }

        public async Task<List<SalesObject>> GetAllSalesObjectsAsync()
        {
            var salesObjects = await _appDBctx.SalesObjects.ToListAsync();
            return salesObjects;
        }

        public async Task<List<SalesObject>> GetSalesObjectsByCountyAsync(County county)
        {
            var salesObjects = await _appDBctx.SalesObjects.Where(s => s.County.Id == county.Id).ToListAsync();
            return salesObjects;
        }

        public async Task<SalesObject> UpdateSalesObjectAsync(SalesObject salesObject)
        {
            _appDBctx.Update(salesObject);
            await _appDBctx.SaveChangesAsync();
            return salesObject;
        }
    }
}
