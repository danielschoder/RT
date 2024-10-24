using Catalog.API.DAL;
using Catalog.API.DTOs;

namespace Catalog.API.BLL;

public class PlatesManager : IPlatesManager
{
    private readonly IPlatesAccessor _platesAccessor;

    public PlatesManager(IPlatesAccessor platesAccessor)
    {
        _platesAccessor = platesAccessor;
    }

    public async Task<IEnumerable<PlateDto>> ListAsync()
    {
        var plates = await _platesAccessor.ListAsync();

        return plates.Select(plate => new PlateDto
        {
            Id = plate.Id,
            Registration = plate.Registration,
            PurchasePrice = plate.PurchasePrice,
            SalePrice = plate.SalePrice,
            Letters = plate.Letters,
            Numbers = plate.Numbers
        });
    }

    public async Task<PlateDto?> GetAsync(Guid id)
    {
        var plate = await _platesAccessor.GetAsync(id);
        if (plate is null) { return null; }

        return new PlateDto
        {
            Id = plate.Id,
            Registration = plate.Registration,
            PurchasePrice = plate.PurchasePrice,
            SalePrice = plate.SalePrice,
            Letters = plate.Letters,
            Numbers = plate.Numbers
        };
    }
}
