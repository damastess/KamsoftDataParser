using System.Text.Json.Serialization;

namespace DataParserApi.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OperationStatus
{
    SUCCESS,
    FAIL,
    ERROR
}