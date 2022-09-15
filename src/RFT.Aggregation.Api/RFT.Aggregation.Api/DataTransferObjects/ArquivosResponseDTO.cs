namespace RFT.Aggregation.Api.DataTransferObjects
{
    public class ArquivosResponseDTO
    {
        public List<ArquivoEstatisticaDTO> ArquivosLidos { get; private set; }

        public ArquivosResponseDTO() => ArquivosLidos = new();
    }
}
