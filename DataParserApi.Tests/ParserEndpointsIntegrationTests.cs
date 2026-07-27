using System.Net;
using System.Net.Http.Json;
using DataParserApi.Models; 
using Microsoft.AspNetCore.Mvc.Testing;

namespace DataParserApi.Tests;

public class ParserEndpointsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ParserEndpointsIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_ParseContent_WithValidCsv_ReturnsOkAndExpectedResponse()
    {
        // Arrange
        var request = new ParseRequest
        {
            Type = ContentType.CSV,
            Content = "aWQsbmFtZQoxLFRlc3Q=" 
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/parse-content", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ParseResponse>();
        
        Assert.NotNull(result);
        Assert.Equal(OperationStatus.SUCCESS, result.Status);
        Assert.Equal(1, result.ProcessedCount);
    }

    [Fact]
    public async Task Post_ParseContent_WithEmptyContent_ReturnsBadRequest()
    {
        // Arrange
        var request = new
        {
            type = "CSV",
            content = "" 
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/parse-content", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
        var result = await response.Content.ReadFromJsonAsync<ParseResponse>();
        Assert.NotNull(result);
        Assert.Equal(OperationStatus.FAIL, result.Status);
        Assert.Contains("Content is empty", result.Data?.ToString());
    }
}