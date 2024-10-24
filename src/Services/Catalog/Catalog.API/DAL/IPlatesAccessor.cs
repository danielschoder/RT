namespace Catalog.API.DAL;

public interface IPlatesAccessor
{
    Task CreateAsync(Plate plate);

    Task<IEnumerable<Plate>> ListAsync();

    Task<Plate?> GetAsync(Guid id);

    Task SaveChangesAsync();
}
