using RFT.Aggregation.Api.IntegrationHandler.Events;
using RFT.Aggregation.Api.MessageBus;
using RFT.Aggregation.Api.Services;

namespace RFT.Aggregation.Api.IntegrationHandler
{
    public class FileIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IContabilizarArquivoService _contabilizarArquivoService;

        public FileIntegrationHandler(IMessageBus messageBus, IContabilizarArquivoService contabilizarArquivoService)
        {
            _messageBus = messageBus;
            _contabilizarArquivoService = contabilizarArquivoService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RegisterSubscribers();
            return Task.CompletedTask;
        }

        private void RegisterSubscribers()
        {            
            _messageBus.ConsumeQueue<ArquivoRecebidoEvento>("ArquivoRecebido", message => ContabilizarArquivo(message));
        }

        private void ContabilizarArquivo(ArquivoRecebidoEvento arquivoRecebidoEvento) => 
            _contabilizarArquivoService.Adicionar(arquivoRecebidoEvento);
    }
}
