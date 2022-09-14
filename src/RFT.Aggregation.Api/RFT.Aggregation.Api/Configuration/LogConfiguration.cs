using Serilog;

namespace RFT.Aggregation.Api.Configuration
{
    public static class LogConfiguration
    {
        public static void AddLogConfiguration(this IServiceCollection services)
        {
            var enviroment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Override("RFT.Aggregation.Api.EventHandler", Serilog.Events.LogEventLevel.Information)
               .Enrich.FromLogContext()
               .Enrich.WithProperty("ApplicationName", $"API Serilog - {enviroment}")
               .WriteTo.File($"/var/log/rft/rft_api_{enviroment}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt")
               .CreateLogger();
        }
    }
}
