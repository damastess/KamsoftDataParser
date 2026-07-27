using DataParserApi.Endpoints;
using DataParserApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDataParserService, DataParserService>();
builder.Services.AddScoped<IFormatParser, CsvFormatParser>();
builder.Services.AddScoped<IFormatParser, JsonFormatParser>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapParserEndpoints();
app.Run();
public partial class Program { }