using RFT.Aggregation.Api.EventHandler;
using RFT.Aggregation.Api.IntegrationHandler.Events;

namespace RFT.Aggregation.Api.Services
{
    public interface IContabilizarArquivoService
    {
        Dictionary<Tuple<string, string>, ArquivoEstatisticaEvento> EventosArmazenados { get; }
        void Adicionar(ArquivoRecebidoEvento arquivoRecebidoEvento);        
    }

    public class ContabilizarArquivoService : IContabilizarArquivoService
    {
        private readonly Dictionary<Tuple<string, string>, ArquivoEstatisticaEvento> _contadores;
        private readonly IArquivoContabilizadoPublisher _arquivoContabilizadoPublisher;

        public ContabilizarArquivoService(IArquivoContabilizadoPublisher arquivoContabilizadoPublisher)
        {
            _contadores = new Dictionary<Tuple<string, string>, ArquivoEstatisticaEvento>();
            _arquivoContabilizadoPublisher = arquivoContabilizadoPublisher;
        }

        public Dictionary<Tuple<string, string>, ArquivoEstatisticaEvento> EventosArmazenados => _contadores;

        public void Adicionar(ArquivoRecebidoEvento arquivoRecebidoEvento)
        {
            ArquivoEstatisticaEvento eventoEstatistica = new ArquivoEstatisticaEvento(arquivoRecebidoEvento);

            if (!_contadores.ContainsKey(eventoEstatistica.Chave))
                _contadores.Add(eventoEstatistica.Chave, eventoEstatistica);
            else
            {
                var contadorSalvo = _contadores[eventoEstatistica.Chave];
                
                contadorSalvo.AtualizarData(eventoEstatistica.Data);
                contadorSalvo.Adicionar();

                eventoEstatistica = contadorSalvo;
            }

            _arquivoContabilizadoPublisher.SendEvent(eventoEstatistica);
        }
    }
}
