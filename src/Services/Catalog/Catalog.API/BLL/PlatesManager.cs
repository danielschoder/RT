using Catalog.API.DAL;
using Catalog.API.DTOs;
using Mapster;

namespace Catalog.API.BLL;

public class PlatesManager : IPlatesManager
{
    private readonly IPlatesAccessor _platesAccessor;

    public PlatesManager(IPlatesAccessor platesAccessor)
    {
        _platesAccessor = platesAccessor;
    }

    public async Task<PlateDto> CreateAsync(PlateDto newPlate)
    {
        var plateEntity = newPlate.Adapt<Plate>();

        await _platesAccessor.CreateAsync(plateEntity);
        await _platesAccessor.SaveChangesAsync();

        return plateEntity.Adapt<PlateDto>();
    }

    public async Task<IEnumerable<PlateDto>> ListAsync()
    {
        var plates = await _platesAccessor.ListAsync();

        return plates.Adapt<IEnumerable<PlateDto>>();
    }

    public async Task<PaginatedResult<PlateDto>> ListAsync(int pageNumber, int pageSize)
    {
        var (plates, totalRecords) = await _platesAccessor.ListAsync(pageNumber, pageSize);

        return new PaginatedResult<PlateDto>
        {
            Data = plates.Adapt<IEnumerable<PlateDto>>(),
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }

    public async Task<PlateDto?> GetAsync(Guid id)
    {
        var plate = await _platesAccessor.GetAsync(id);
        if (plate is null) { return null; }

        return plate.Adapt<PlateDto>();
    }
}
