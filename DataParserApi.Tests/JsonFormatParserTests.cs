using System.Text.Json;
using DataParserApi.Models;
using DataParserApi.Services; 

namespace DataParserApi.Tests;

public class JsonFormatParserTests
{
    private readonly JsonFormatParser _sut;

    public JsonFormatParserTests()
    {
        _sut = new JsonFormatParser();
    }

    [Fact]
    public void Parse_ValidJsonArray_ReturnsSuccessAndExpectedCount()
    {
        // Arrange
        var validJsonArray = @"[{ ""id"": 1, ""name"": ""Test1"" }, { ""id"": 2, ""name"": ""Test2"" }]";

        // Act
        var response = _sut.Parse(validJsonArray);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(OperationStatus.SUCCESS, response.Status);
        Assert.Equal(2, response.ProcessedCount);
        
        var dataList = response.Data as List<object>;
        Assert.NotNull(dataList);
        Assert.Equal(2, dataList.Count);
    }

    [Fact]
    public void Parse_ValidSingleJsonObject_ReturnsSuccessAndCountOfOne()
    {
        // Arrange
        var validJsonObject = @"{ ""id"": 1, ""name"": ""SingleTest"" }";

        // Act
        var response = _sut.Parse(validJsonObject);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(OperationStatus.SUCCESS, response.Status);
        
        Assert.Equal(1, response.ProcessedCount);
        
        var dataList = response.Data as List<object>;
        Assert.NotNull(dataList);
        Assert.Single(dataList);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Parse_EmptyOrNullJsonString_ReturnsSuccessWithZeroCount(string? emptyJson)
    {
        // Act
        var response = _sut.Parse(emptyJson!);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(OperationStatus.SUCCESS, response.Status);
        Assert.Equal(0, response.ProcessedCount);
    }

    [Fact]
    public void Parse_JsonWithInvalidValueKind_ThrowsJsonException()
    {
        // Arrange
        var invalidKindJson = "\"Just a primitive string\"";

        // Act & Assert
        var exception = Assert.Throws<JsonException>(() => _sut.Parse(invalidKindJson));
        Assert.Contains("must be an object or an array", exception.Message);
    }

    [Fact]
    public void Parse_MalformedJsonString_ThrowsJsonException()
    {
        // Arrange
        var malformedJson = "{ to nie jest poprawny json";

        // Act & Assert
        Assert.ThrowsAny<JsonException>(() => _sut.Parse(malformedJson));
    }
}