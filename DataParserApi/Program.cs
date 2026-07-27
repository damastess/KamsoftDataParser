using DataParserApi.Endpoints;
using DataParserApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDataParserService, DataParserService>();
var app = builder.Build();
app.MapParserEndpoints();
app.Run();