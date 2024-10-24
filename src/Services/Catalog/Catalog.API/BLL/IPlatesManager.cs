using Catalog.API.DTOs;

namespace Catalog.API.BLL;

public interface IPlatesManager
{
    Task<PlateDto> CreateAsync(PlateDto newPlate);

    Task<IEnumerable<PlateDto>> ListAsync();

    Task<PlateDto?> GetAsync(Guid id);
}
