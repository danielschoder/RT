using Catalog.API.DAL;
using Catalog.API.Data;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Tests.DAL;

[TestFixture]
public class PlatesAccessorTests
{
    private ApplicationDbContext _dbContext;
    private PlatesAccessor _platesAccessor;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _platesAccessor = new PlatesAccessor(_dbContext);

        var plates = new List<Plate>
        {
            new Plate { Id = Guid.NewGuid(), Registration = "ABC123", PurchasePrice = 100.00m, SalePrice = 150.00m, Letters = "ABC", Numbers = 123 },
            new Plate { Id = Guid.NewGuid(), Registration = "XYZ789", PurchasePrice = 200.00m, SalePrice = 250.00m, Letters = "XYZ", Numbers = 456 }
        };

        _dbContext.Plates.AddRange(plates);
        _dbContext.SaveChanges();
    }

    [Test]
    public async Task ListAsync_ReturnsPlates()
    {
        // Act
        var result = await _platesAccessor.ListAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<IEnumerable<Plate>>());
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
    }
}
