using Microsoft.Extensions.DependencyInjection;
using RFT.Aggregation.Api.EventHandler;
using RFT.Aggregation.Api.Services;

namespace RFT.Aggregation.Api.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServicesApi(this IServiceCollection services)
        {
            return services.AddSingleton<IContabilizarArquivoService, ContabilizarArquivoService>()
                           .AddSingleton<IArquivoContabilizadoPublisher, ArquivoContabilizadoPublisher>()
                           .AddHostedService<ArquivoContabilizadoLogSubscribe>();
        }
    }
}
