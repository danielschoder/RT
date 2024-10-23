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
        var plates = new List<Plate>
        {
            new() { Id = Guid.NewGuid(), Registration = "ABC123", PurchasePrice = 100.00m, SalePrice = 150.00m, Letters = "ABC", Numbers = 123 },
            new() { Id = Guid.NewGuid(), Registration = "XYZ789", PurchasePrice = 200.00m, SalePrice = 250.00m, Letters = "XYZ", Numbers = 789 }
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
            Assert.That(plateDtos[0].Registration, Is.EqualTo("ABC123"));
            Assert.That(plateDtos[0].Numbers, Is.EqualTo(123));
            Assert.That(plateDtos[0].SalePrice, Is.EqualTo(150.00m));
        });
        Assert.Multiple(() =>
        {
            Assert.That(plateDtos[1].Registration, Is.EqualTo("XYZ789"));
            Assert.That(plateDtos[1].Numbers, Is.EqualTo(789));
            Assert.That(plateDtos[1].SalePrice, Is.EqualTo(250.00m));
        });
        _mockPlatesAccessor.Verify(pa => pa.ListAsync(), Times.Once);
    }
}
