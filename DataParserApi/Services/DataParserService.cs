using System.Text;
using System.Text.Json;
using CsvHelper;
using DataParserApi.Models;

namespace DataParserApi.Services;

public class DataParserService : IDataParserService
{
    private readonly IEnumerable<IFormatParser> _parsers;

    public DataParserService(IEnumerable<IFormatParser> parsers)
    {
        _parsers = parsers;
    }

    public ParseResponse ProcessData(ParseRequest request)
    {
        try
        {
            var decodedContent = DecodeBase64(request.Content);
            var parser = _parsers.FirstOrDefault(p => p.CanParse(request.Type));
            if (parser == null)
            {
                return new ParseResponse()
                {
                    Status = OperationStatus.FAIL,
                    ProcessedCount = 0,
                    Data = $"Unsupported data type {request.Type}"
                };
            }
            return parser.Parse(decodedContent);
        }
        catch (FormatException ex)
        {
            return new ParseResponse()
            {
                Status = OperationStatus.ERROR,
                ProcessedCount = 0,
                Data = $"Invalid Base64 data format: {ex.Message}"
            };
        }
        catch (JsonException ex)
        {
            return new ParseResponse()
            {
                Status = OperationStatus.ERROR,
                ProcessedCount = 0,
                Data = $"Invalid JSON format: {ex.Message}"
            };
        }
        catch (CsvHelperException ex)
        {
            return new ParseResponse()
            {
                Status = OperationStatus.ERROR,
                ProcessedCount = 0,
                Data = $"Invalid CSV format: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ParseResponse()
            {
                Status = OperationStatus.ERROR,
                ProcessedCount = 0,
                Data = $"An unexpected error occurred: {ex.Message}"
            };
        }
    }

    private static string DecodeBase64(string base64String)
    {
        byte[] data = Convert.FromBase64String(base64String);
        return Encoding.UTF8.GetString(data);
    }
}