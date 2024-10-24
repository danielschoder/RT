using Catalog.API.BLL;
using Catalog.API.Controllers;
using Catalog.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Catalog.API.Tests.Controllers;

[TestFixture]
public class PlatesControllerTests
{
    private PlatesController _controller;
    private Mock<IPlatesManager> _mockPlatesManager;

    [SetUp]
    public void Setup()
    {
        _mockPlatesManager = new Mock<IPlatesManager>();
        _controller = new PlatesController(_mockPlatesManager.Object);
    }

    [Test]
    public async Task GetPlates_ReturnsOkResult_WithListOfPlateDtos()
    {
        // Arrange
        var plateDtos = new List<PlateDto>
        {
            new() { Id = Guid.NewGuid(), Registration = "ABC123", PurchasePrice = 100.00m, SalePrice = 150.00m, Letters = "ABC", Numbers = 123 },
        };

        _mockPlatesManager
            .Setup(pm => pm.GetPlatesAsync())
            .ReturnsAsync(plateDtos);

        // Act
        var result = await _controller.GetPlates();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>()); // Check if the result is OkObjectResult

        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.InstanceOf<IEnumerable<PlateDto>>());

        var returnedPlates = okResult.Value as IEnumerable<PlateDto>;
        Assert.That(returnedPlates?.Count(), Is.EqualTo(plateDtos.Count));

        var firstPlate = returnedPlates.First();
        Assert.Multiple(() =>
        {
            Assert.That(firstPlate.Id, Is.EqualTo(plateDtos[0].Id));
            Assert.That(firstPlate.Registration, Is.EqualTo(plateDtos[0].Registration));
            Assert.That(firstPlate.PurchasePrice, Is.EqualTo(plateDtos[0].PurchasePrice));
            Assert.That(firstPlate.SalePrice, Is.EqualTo(plateDtos[0].SalePrice));
            Assert.That(firstPlate.Letters, Is.EqualTo(plateDtos[0].Letters));
            Assert.That(firstPlate.Numbers, Is.EqualTo(plateDtos[0].Numbers));
        });

        _mockPlatesManager.Verify(pm => pm.GetPlatesAsync(), Times.Once);
    }

    [Test]
    public async Task GetPlate_ValidId_ReturnsOkResultWithPlateDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var plateDto = new PlateDto
        {
            Id = id,
            Registration = "ABC123",
            PurchasePrice = 100.00m,
            SalePrice = 150.00m,
            Letters = "ABC",
            Numbers = 123
        };

        _mockPlatesManager
            .Setup(pm => pm.GetAsync(id))
            .ReturnsAsync(plateDto);

        // Act
        var result = await _controller.GetPlate(id);

        // Assert
        Assert.That(result?.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        var plate = okResult?.Value as PlateDto;
        Assert.Multiple(() =>
        {
            Assert.That(plate, Is.InstanceOf<PlateDto>());
            Assert.That(plate?.Id, Is.EqualTo(id));
        });
    }

    [Test]
    public async Task GetPlate_InvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        PlateDto? nullPlateDto = null;

        _mockPlatesManager
            .Setup(pm => pm.GetAsync(id))
            .ReturnsAsync(nullPlateDto);

        // Act
        var result = await _controller.GetPlate(id);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }
}
