using Catalog.API.BLL;
using Catalog.API.DAL;
using Catalog.API.DTOs;
using Catalog.Domain;
using Moq;

namespace Catalog.API.Tests.BLL;

[TestFixture]
public class PlatesManagerTests
{
    private Mock<IPlatesAccessor> _mockPlatesAccessor;
    private PlatesManager _platesManager;

    [SetUp]
    public void Setup()
    {
        _mockPlatesAccessor = new Mock<IPlatesAccessor>();
        _platesManager = new PlatesManager(_mockPlatesAccessor.Object);
    }

    [Test]
    public async Task GetPlatesAsync_ReturnsPlateDtos()
    {
        // Arrange
        var id0 = Guid.NewGuid();
        var id1 = Guid.NewGuid();
        var plates = new List<Plate>
        {
            new() { Id = id0, Registration = "ABC123", PurchasePrice = 100.00m, SalePrice = 150.00m, Letters = "ABC", Numbers = 123 },
            new() { Id = id1, Registration = "XYZ789", PurchasePrice = 200.00m, SalePrice = 250.00m, Letters = "XYZ", Numbers = 789 }
        };

        _mockPlatesAccessor
            .Setup(pa => pa.ListAsync())
            .ReturnsAsync(plates);

        // Act
        var result = await _platesManager.GetPlatesAsync();

        // Assert
        Assert.That(result, Is.InstanceOf<IEnumerable<PlateDto>>());
        Assert.That(result.Count(), Is.EqualTo(2));

        var plateDtos = result.ToList();
        Assert.Multiple(() =>
        {
            Assert.That(plateDtos[0].Id, Is.EqualTo(id0));
            Assert.That(plateDtos[0].Registration, Is.EqualTo("ABC123"));
            Assert.That(plateDtos[0].PurchasePrice, Is.EqualTo(100.00m));
            Assert.That(plateDtos[0].SalePrice, Is.EqualTo(150.00m));
            Assert.That(plateDtos[0].Letters, Is.EqualTo("ABC"));
            Assert.That(plateDtos[0].Numbers, Is.EqualTo(123));
        });
        Assert.Multiple(() =>
        {
            Assert.That(plateDtos[1].Id, Is.EqualTo(id1));
            Assert.That(plateDtos[1].Registration, Is.EqualTo("XYZ789"));
            Assert.That(plateDtos[1].PurchasePrice, Is.EqualTo(200.00m));
            Assert.That(plateDtos[1].SalePrice, Is.EqualTo(250.00m));
            Assert.That(plateDtos[1].Letters, Is.EqualTo("XYZ"));
            Assert.That(plateDtos[1].Numbers, Is.EqualTo(789));
        });
        _mockPlatesAccessor.Verify(pa => pa.ListAsync(), Times.Once);
    }

    [Test]
    public async Task GetAsync_ValidId_ReturnsPlateDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var plate = new Plate
        {
            Id = id,
            Registration = "ABC123",
            PurchasePrice = 100.00m,
            SalePrice = 150.00m,
            Letters = "ABC",
            Numbers = 123
        };

        _mockPlatesAccessor
            .Setup(pa => pa.GetAsync(id))
            .ReturnsAsync(plate);

        // Act
        var result = await _platesManager.GetAsync(id);

        // Assert
        Assert.That(result, Is.InstanceOf<PlateDto>());
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Registration, Is.EqualTo("ABC123"));
            Assert.That(result.PurchasePrice, Is.EqualTo(100.00m));
            Assert.That(result.SalePrice, Is.EqualTo(150.00m));
            Assert.That(result.Letters, Is.EqualTo("ABC"));
            Assert.That(result.Numbers, Is.EqualTo(123));
        });
        _mockPlatesAccessor.Verify(pa => pa.GetAsync(id), Times.Once);
    }

    [Test]
    public async Task GetAsync_InvalidId_ReturnsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockPlatesAccessor
            .Setup(pa => pa.GetAsync(id))
            .ReturnsAsync((Plate)null); // Simulate not found

        // Act
        var result = await _platesManager.GetAsync(id);

        // Assert
        Assert.That(result, Is.Null);
        _mockPlatesAccessor.Verify(pa => pa.GetAsync(id), Times.Once);
    }
}
