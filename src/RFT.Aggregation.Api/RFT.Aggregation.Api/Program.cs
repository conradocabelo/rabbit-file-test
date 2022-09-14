using RFT.Aggregation.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApiConfiguration();
builder.Services.AddMessageBus(builder.Configuration);
builder.Services.AddServicesApi();
builder.Services.AddLogConfiguration();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);

app.Run();