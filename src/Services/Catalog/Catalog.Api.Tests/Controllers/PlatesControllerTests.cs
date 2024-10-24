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
    public async Task GetPlates_ReturnsOkResult_WithExpectedPaginatedResult()
    {
        // Arrange
        var expectedPlates = new PaginatedResult<PlateDto>
        {
            Data = new List<PlateDto>
            {
                new() { Id = Guid.NewGuid(), Registration = "ABC123" },
                new() { Id = Guid.NewGuid(), Registration = "XYZ789" }
            },
            CurrentPage = 1,
            PageSize = 20,
            TotalRecords = 2
        };

        _mockPlatesManager.Setup(m => m.ListAsync(1, 20, "RegistrationAsc"))
                          .ReturnsAsync(expectedPlates);

        // Act
        var result = await _controller.GetPlates();

        // Assert
        var okResult = result.Result as OkObjectResult;
        var paginatedResult = okResult?.Value as PaginatedResult<PlateDto>;
        Assert.Multiple(() =>
        {
            Assert.That(paginatedResult, Is.InstanceOf<PaginatedResult<PlateDto>>());
            Assert.That(paginatedResult?.CurrentPage, Is.EqualTo(expectedPlates.CurrentPage));
            Assert.That(paginatedResult?.PageSize, Is.EqualTo(expectedPlates.PageSize));
            Assert.That(paginatedResult?.TotalRecords, Is.EqualTo(expectedPlates.TotalRecords));
            Assert.That(paginatedResult?.Data.Count(), Is.EqualTo(expectedPlates.Data.Count()));
        });
    }

    [Test]
    public async Task GetPlates_CallsListAsync_WithCorrectParameters()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 20;
        const string sortOrder = "RegistrationAsc";

        // Act
        await _controller.GetPlates(pageNumber, pageSize, sortOrder);

        // Assert
        _mockPlatesManager.Verify(m => m.ListAsync(pageNumber, pageSize, sortOrder), Times.Once);
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
