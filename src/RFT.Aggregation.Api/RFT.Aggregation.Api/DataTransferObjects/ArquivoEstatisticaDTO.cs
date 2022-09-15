namespace RFT.Aggregation.Api.DataTransferObjects
{
    public class ArquivoEstatisticaDTO
    {
        public string? NomeMaquina { get; set; }
        public string? NomeAplicacao { get; set; }
        public DateTime DataUltimaLeitura { get; set; }
        public int QuantidadeLeituras { get; set; }
    }
}
