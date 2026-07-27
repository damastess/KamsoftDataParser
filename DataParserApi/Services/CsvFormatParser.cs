using System.Globalization;
using CsvHelper;
using DataParserApi.Models;

namespace DataParserApi.Services;

public class CsvFormatParser : IFormatParser
{
    public bool CanParse(ContentType type) => type == ContentType.CSV;

    public ParseResponse Parse(string csvContent)
    {
        if (string.IsNullOrWhiteSpace(csvContent))
        {
            return new ParseResponse()
            {
                Status = OperationStatus.SUCCESS,
                ProcessedCount = 0,
                Data = new List<dynamic>()
            };
        }

        using var reader = new StringReader(csvContent);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<dynamic>().ToList();
        
        return new ParseResponse()
        {
            Status = OperationStatus.SUCCESS,
            ProcessedCount = records.Count,
            Data = records
        };
    }
}