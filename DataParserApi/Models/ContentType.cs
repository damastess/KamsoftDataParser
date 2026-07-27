using System.Text.Json.Serialization;

namespace DataParserApi.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContentType
{ 
    CSV, 
    INTERNAL_JSON
}