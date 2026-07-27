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
                    return Results.BadRequest(new {error = "Content is required"});
                }

                try
                {
                    var result = parserService.ProcessData(request);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new {error = ex.Message});
                }
            }

        );
    }
}