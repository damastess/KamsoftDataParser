using DataParserApi.Models;

namespace DataParserApi.Services;

public interface IDataParserService
{
    ParseResponse ProcessData(ParseRequest request);
}