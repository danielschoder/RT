using Catalog.API.DTOs;

namespace Catalog.API.BLL
{
    public interface IPlatesManager
    {
        Task<IEnumerable<PlateDto>> GetPlatesAsync();
    }
}