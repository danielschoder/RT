namespace Catalog.API.DAL;

public class PlatesAccessor : IPlatesAccessor
{
    private readonly ApplicationDbContext _dbContext;

    public PlatesAccessor(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Plate>> ListAsync()
    {
        return await _dbContext.Plates
            .AsNoTracking()
            .ToListAsync();
    }
}
