using DataParserApi.Models;

namespace DataParserApi.Services;

public interface IFormatParser
{
    bool CanParse(ContentType type);
    ParseResponse Parse(string content);
}