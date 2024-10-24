using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using WebMVC.Controllers;
using WebMVC.DTOs;

[TestFixture]
public class PlatesControllerTests
{
    private PlatesController _controller;
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private Mock<ILogger<PlatesController>> _mockLogger;
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<PlatesController>>();
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://catalog-api/")
        };

        _controller = new PlatesController(_mockLogger.Object, _httpClient);
    }

    [Test]
    public async Task Details_ReturnsViewResult_WithPlateDto_WhenSuccessStatusCode()
    {
        // Arrange
        var plateId = Guid.NewGuid();
        var expectedPlate = new PlateDto
        {
            Id = plateId,
            Registration = "ABC123"
        };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(expectedPlate))
            });

        // Act
        var result = await _controller.Details(plateId);

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = result as ViewResult;
        var model = viewResult?.Model as PlateDto;

        Assert.That(model, Is.InstanceOf<PlateDto>());
        Assert.Multiple(() =>
        {
            Assert.That(model.Id, Is.EqualTo(expectedPlate.Id));
            Assert.That(model.Registration, Is.EqualTo(expectedPlate.Registration));
        });
    }

    [Test]
    public async Task Details_ReturnsNotFound_WhenResponseIsNotSuccess()
    {
        // Arrange
        var plateId = Guid.NewGuid();

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            });

        // Act
        var result = await _controller.Details(plateId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
