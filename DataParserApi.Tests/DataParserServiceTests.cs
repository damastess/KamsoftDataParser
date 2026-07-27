using DataParserApi.Models;
using DataParserApi.Services;

namespace DataParserApi.Tests;

public class DataParserServiceTests
{
    private readonly DataParserService _sut;

    public DataParserServiceTests()
    {
        var parsers = new List<IFormatParser> 
        { 
            new CsvFormatParser(), 
            new JsonFormatParser() 
        };
        
        _sut = new DataParserService(parsers); 
    }

    [Fact]
    public void ProcessData_WithInvalidBase64_ReturnsErrorStatus()
    {
        // Arrange
        var request = new ParseRequest
        {
            Type = ContentType.CSV,
            Content = "to-nie-jest-prawidlowy-kod-base64!@#"
        };

        // Act
        var result = _sut.ProcessData(request);

        // Assert
        Assert.Equal(OperationStatus.ERROR, result.Status);
        Assert.Equal(0, result.ProcessedCount);
        Assert.Contains("Invalid Base64", result.Data?.ToString());
    }

    [Fact]
    public void ProcessData_WithUnsupportedContentType_ReturnsFailStatus()
    {
        // Arrange
        var request = new ParseRequest
        {
            Type = (ContentType)999, 
            Content = "aWQsbmFtZQoxLFRlc3Q="
        };

        // Act
        var result = _sut.ProcessData(request);

        // Assert
        Assert.Equal(OperationStatus.FAIL, result.Status);
        Assert.Equal(0, result.ProcessedCount);
        Assert.Contains("Unsupported data type", result.Data?.ToString());
    }

    [Fact]
    public void ProcessData_WhenParserThrowsJsonException_ReturnsErrorStatus()
    {
        // Arrange
        var request = new ParseRequest
        {
            Type = ContentType.INTERNAL_JSON,
            Content = "aW52YWxpZC1qc29uLWRhdGE=" 
        };

        // Act
        var result = _sut.ProcessData(request);

        // Assert
        Assert.Equal(OperationStatus.ERROR, result.Status);
        Assert.Equal(0, result.ProcessedCount);
        Assert.Contains("Invalid JSON format", result.Data?.ToString()); 
    }

    [Fact]
    public void ProcessData_WithValidRequest_ReturnsSuccessStatus()
    {
        // Arrange
        var request = new ParseRequest
        {
            Type = ContentType.CSV,
            Content = "aWQsbmFtZQoxLFRlc3Q=" 
        };

        // Act
        var result = _sut.ProcessData(request);

        // Assert
        Assert.Equal(OperationStatus.SUCCESS, result.Status);
        Assert.True(result.ProcessedCount > 0);
        Assert.NotNull(result.Data);
    }
}