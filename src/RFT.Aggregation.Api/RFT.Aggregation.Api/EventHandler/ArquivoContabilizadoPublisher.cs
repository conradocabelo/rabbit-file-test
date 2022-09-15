using RFT.Aggregation.Api.IntegrationHandler.Events;

namespace RFT.Aggregation.Api.EventHandler
{
    public interface IArquivoContabilizadoPublisher
    {
        event EventHandler<ArquivoEstatisticaEvento> RaiseEvent;

        void SendEvent(ArquivoEstatisticaEvento messageEvent);
    }

    public class ArquivoContabilizadoPublisher : IArquivoContabilizadoPublisher
    {
        public event EventHandler<ArquivoEstatisticaEvento>? RaiseEvent;

        protected void OnRaiseEvent(ArquivoEstatisticaEvento messageEvent) =>
              RaiseEvent?.Invoke(this, messageEvent);

        public void SendEvent(ArquivoEstatisticaEvento messageEvent)
        {
            if (messageEvent == null)
                throw new NullReferenceException();

            OnRaiseEvent(messageEvent);
        }
    }
}
