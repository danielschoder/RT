using Catalog.API.DTOs;

namespace Catalog.API.BLL;

public interface IPlatesManager
{
    Task<IEnumerable<PlateDto>> ListAsync();

    Task<PlateDto?> GetAsync(Guid id);
}
