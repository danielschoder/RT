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

    public async Task<(IEnumerable<Plate> Plates, int TotalRecords)> ListAsync(
        int pageNumber,
        int pageSize,
        string sortOrder)
    {
        var query = _dbContext.Plates.AsNoTracking();

        query = sortOrder switch
        {
            "SalePriceAsc" => query.OrderBy(p => p.SalePrice).ThenBy(p => p.Registration),
            "SalePriceDesc" => query.OrderByDescending(p => p.SalePrice).ThenBy(p => p.Registration),
            "RegistrationAsc" => query.OrderBy(p => p.Registration),
            "RegistrationDesc" => query.OrderByDescending(p => p.Registration),
            _ => query
        };
        
        var totalRecords = await query.CountAsync();

        var plates = await query
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (plates, totalRecords);
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
