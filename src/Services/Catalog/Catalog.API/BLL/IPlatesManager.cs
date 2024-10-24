using Catalog.API.DTOs;

namespace Catalog.API.BLL;

public interface IPlatesManager
{
    Task<PlateDto> CreateAsync(PlateDto newPlate);

    Task<IEnumerable<PlateDto>> ListAsync();

    Task<PaginatedResult<PlateDto>> ListAsync(int pageNumber, int pageSize, string sortOrder);

    Task<PlateDto?> GetAsync(Guid id);
}
