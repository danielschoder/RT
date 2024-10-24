namespace Catalog.API.DAL;

public interface IPlatesAccessor
{
    Task CreateAsync(Plate plate);

    Task<IEnumerable<Plate>> ListAsync();

    Task<(IEnumerable<Plate> Plates, int TotalRecords)> ListAsync(int pageNumber, int pageSize);

    Task<Plate?> GetAsync(Guid id);

    Task SaveChangesAsync();
}
