using System.Text.Json;
using DataParserApi.Models;

namespace DataParserApi.Services;

public class JsonFormatParser : IFormatParser
{
    public bool CanParse(ContentType type) => type == ContentType.INTERNAL_JSON;

    public ParseResponse Parse(string jsonContent)
    {
        if (string.IsNullOrWhiteSpace(jsonContent))
        {
            return new ParseResponse()
            {
                Status = OperationStatus.SUCCESS,
                ProcessedCount = 0,
                Data = new List<dynamic>()
            };
        }

        using var document = JsonDocument.Parse(jsonContent);
        var root = document.RootElement;
        object? parsedData;
        int count = 0;

        if (root.ValueKind == JsonValueKind.Array)
        {
            parsedData = JsonSerializer.Deserialize<List<object>>(jsonContent);
            count = root.GetArrayLength();
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            var singleObject = JsonSerializer.Deserialize<object>(jsonContent);
            parsedData = new List<object> {singleObject!};
            count = 1;
        }
        else
        {
            throw new JsonException("Json payload must be an object or an array of objects.");
        }
        return new ParseResponse()
        {
            Status = OperationStatus.SUCCESS,
            ProcessedCount = count,
            Data = parsedData
        };
    }
}