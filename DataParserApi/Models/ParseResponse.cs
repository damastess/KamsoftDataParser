namespace DataParserApi.Models;

public class ParseResponse
{
    public required OperationStatus Status { get; set; }
    public int ProcessedCount { get; set; }
    public object? Data { get; set; }
}