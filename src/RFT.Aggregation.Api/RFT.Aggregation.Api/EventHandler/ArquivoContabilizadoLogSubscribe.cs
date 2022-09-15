using RFT.Aggregation.Api.IntegrationHandler.Events;

namespace RFT.Aggregation.Api.EventHandler
{
    public class ArquivoContabilizadoLogSubscribe: BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IArquivoContabilizadoPublisher _publisher;

        public ArquivoContabilizadoLogSubscribe(IArquivoContabilizadoPublisher publisher, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _publisher = publisher;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _publisher.RaiseEvent += OnHandlerEvent;
            return Task.CompletedTask;
        }

        protected void OnHandlerEvent(object? sender, ArquivoEstatisticaEvento? e)
        {
            if (e != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ArquivoContabilizadoLogSubscribe>>();
                    logger.LogInformation($"count = {e.Quantidade} for file {e.NomeAplicacao} {e.Data}");
                }
            }
        }
    }
}
