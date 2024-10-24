namespace Catalog.API.DAL;

public class PlatesAccessor : IPlatesAccessor
{
    private readonly ApplicationDbContext _dbContext;

    public PlatesAccessor(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateAsync(Plate plate)
    {
        await _dbContext.Plates.AddAsync(plate);
    }

    public async Task<IEnumerable<Plate>> ListAsync()
    {
        return await _dbContext.Plates
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Plate?> GetAsync(Guid id)
    {
        return await _dbContext.Plates
            .AsNoTracking()
            .FirstOrDefaultAsync(plate => plate.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
