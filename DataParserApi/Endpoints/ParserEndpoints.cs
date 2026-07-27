using DataParserApi.Models;
using DataParserApi.Services;

namespace DataParserApi.Endpoints;

public static class ParserEndpoints
{
    public static void MapParserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/parse-content", (ParseRequest request, IDataParserService parserService) =>
            {
                if (string.IsNullOrWhiteSpace(request.Content))
                {
                    var errorResponse = new ParseResponse()
                    {
                        Status = OperationStatus.FAIL,
                        ProcessedCount = 0,
                        Data = $"Content is empty"
                    };
                    return Results.BadRequest(errorResponse);
                }

                try
                {
                    var result = parserService.ProcessData(request);

                    if (result.Status != OperationStatus.SUCCESS)
                    {
                        return Results.BadRequest(result);
                    }
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new {error = ex.Message});
                }
            }

        ).WithOpenApi(operation =>
        {
            operation.Summary = "Parses BASE64 encoded data from CSV or internal JSON.";
            operation.Description = "Accepts a BASE64 encoded payload, decodes it, and parses the underlying data based on the provided content type (CSV or INTERNAL_JSON). Returns the processed data in a unified JSON structure.";
            return operation;
        });
        }
}