
using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    /* Interface for API requests and responses related to SalesObjects.
     * Author: Tobias 2024-04-24
     * 
     * Update: Added GetAllByRealtor method. Changed name of GetAll(countId) to GetAllByCounty / Tobias 2024-04-29
     * Update: Updated Update method to return Task<SalesObjectDTO> / Tobias 2024-05-03
     */

    public interface ISalesObject
    {

        Task<SalesObjectDTO> Get(int id);

        Task<List<SalesObjectDTO>> GetAll();

        Task<List<SalesObjectDTO>> GetAllByCounty(int id);

        Task<List<SalesObjectDTO>> GetAllByRealtor(int id);

        Task<SalesObjectDTO> Create(SalesObjectDTO salesObject);

        Task<SalesObjectDTO> Update(int id, SalesObjectDTO salesObject);

        Task Delete(int id);




        Task<List<SalesObjectDTO>> GetSalesByCounty(string name);


    }
}
