
namespace Catalog.API.DAL
{
    public interface IPlatesAccessor
    {
        Task<IEnumerable<Plate>> ListAsync();
    }
}