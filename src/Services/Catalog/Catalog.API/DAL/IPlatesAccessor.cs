namespace Catalog.API.DAL;

public interface IPlatesAccessor
{
    Task<IEnumerable<Plate>> ListAsync();

    Task<Plate?> GetAsync(Guid id);
}
