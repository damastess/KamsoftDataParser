using DataParserApi.Models;
using DataParserApi.Services;

namespace DataParserApi.Tests;

public class CsvFormatParserTests
{
    private readonly CsvFormatParser _sut;

    public CsvFormatParserTests()
    {
        _sut = new CsvFormatParser();
    }

    [Fact]
    public void Parse_ValidCsvString_ReturnsSuccessAndExpectedCount()
    {
        // Arrange
        var validCsv = "Id,Name\n1,Alice\n2,Bob";

        // Act
        var response = _sut.Parse(validCsv);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(OperationStatus.SUCCESS, response.Status);
        Assert.Equal(2, response.ProcessedCount);
        var dataList = response.Data as IEnumerable<dynamic>;
        Assert.NotNull(dataList);
        Assert.Equal(2, dataList.Count());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Parse_EmptyOrNullCsvString_ReturnsSuccessWithZeroCount(string? emptyCsv)
    {
        // Act
        var response = _sut.Parse(emptyCsv!);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(OperationStatus.SUCCESS, response.Status);
        Assert.Equal(0, response.ProcessedCount);
        
        var dataList = response.Data as IEnumerable<dynamic>;
        Assert.NotNull(dataList);
        Assert.Empty(dataList);
    }
}