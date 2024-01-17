using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;
using Rainfall.Api.Models;
using Rainfall.Api.Web.Controllers;
using Rainfall.Api.Web.Services;

namespace Rainfall.Api.Test;
using NSubstitute;

public class RainFallControllerTests
{
    private readonly IRainFallReaderService _mockRainFallReaderService;
    private readonly RainfallController _rainfallController;
    
    public RainFallControllerTests()
    {
        _mockRainFallReaderService = Substitute.For<IRainFallReaderService>();
        _rainfallController = new RainfallController(_mockRainFallReaderService);
    }

    [Fact]
    public async Task GetRainfallReadings_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        _mockRainFallReaderService.GetRainfallReadings(Arg.Any<string>(), Arg.Any<int>())
            .Returns(new RainfallReadingResponse { Readings = new List<RainfallReading>() });
        
        // Act
        var result = await _rainfallController.GetRainfallReadings("stationId", 10); // set some stationId before run it

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.IsType<RainfallReadingResponse>(okResult.Value);
    }

    [Fact]
    public async Task GetRainfallReadings_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        _mockRainFallReaderService.GetRainfallReadings(Arg.Any<string>(), Arg.Any<int>())
            .Throws(new ArgumentException());

        // Act
        var result = await _rainfallController.GetRainfallReadings("invalidStationId", 10);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetRainfallReadings_NoReadings_ReturnsNotFound()
    {
        // Arrange
        _mockRainFallReaderService.GetRainfallReadings(Arg.Any<string>(), Arg.Any<int>())
            .Returns(new RainfallReadingResponse { Readings = new List<RainfallReading>() });

        // Act
        var result = await _rainfallController.GetRainfallReadings("stationId", 10);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetRainfallReadings_InternalServerError_ReturnsInternalServerError()
    {
        // Arrange
        _mockRainFallReaderService.GetRainfallReadings(Arg.Any<string>(), Arg.Any<int>()).Throws(new Exception("Some error"));

        // Act
        var result = await _rainfallController.GetRainfallReadings("stationId", 10);

        // Assert
        Assert.IsType<ObjectResult>(result);
        var objectResult = (ObjectResult)result;
        Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        Assert.IsType<Error>(objectResult.Value);
    }
}