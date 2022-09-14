using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace RFT.Aggregation.Api.IntegrationHandler.Events
{
    public class ArquivoEstatisticaEvento
    {
        public Tuple<string, string> Chave { get; private set; }
        public string NomeMaquina { get; private set; }
        public string NomeAplicacao { get; private set; }
        public DateTime Data { get; private set; }
        public int Quantidade { get; private set; }
       

        public ArquivoEstatisticaEvento(ArquivoRecebidoEvento arquivoRecebidoEvento)
        {
            ConstruirEvento(arquivoRecebidoEvento);
        }

        private void ConstruirEvento(ArquivoRecebidoEvento arquivoRecebidoEvento)
        {
            var elements = new Regex(@"^[.]|[_]|.[^.]+$").Split(arquivoRecebidoEvento.filename);

            NomeMaquina = elements[0];
            NomeAplicacao = elements[1];
            Data = DateTime.ParseExact(elements[2], "yyyyMMddHHmms", null);
            Quantidade = 1;

            Chave = new Tuple<string, string>(NomeMaquina, NomeAplicacao);
        }

        public void Adicionar() => Quantidade += 1;

        public void AtualizarData(DateTime data) => Data = data;
    }
}
