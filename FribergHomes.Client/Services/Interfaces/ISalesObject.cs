
using FribergHomes.Client.DTOs;

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

        Task<SalesObjectDTO> Create(SalesObjectDTO salesObject);

        Task Update(int id, SalesObjectDTO salesObject);

        Task Delete(int id);

    }
}
