namespace DataParserApi.Models;

public class ParseRequest
{
    public required ContentType Type {get; set;}
    public required string Content {get; set;}
}