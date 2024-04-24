using FribergHomes.API.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    /* Interface for API requests and responses related to SalesObjects.
     * Author: Tobias 2024-04-24
     */

    public interface ISalesObject
    {

        Task<SalesObjectDTO> Get(int id);

        Task<List<SalesObjectDTO>> GetAll();

        Task<List<SalesObjectDTO>> GetAll(int countyId);

        Task Create(SalesObjectDTO salesObject);

        Task<SalesObjectDTO> Update(int id, SalesObjectDTO salesObject);

        Task<SalesObjectDTO> Delete(int id);

    }
}
