using Microsoft.AspNetCore.Mvc;
using RFT.Aggregation.Api.DataTransferObjects;
using RFT.Aggregation.Api.IntegrationHandler.Events;
using RFT.Aggregation.Api.Services;

namespace RFT.Aggregation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivosContabilizadosController : ControllerBase
    {
        private readonly IContabilizarArquivoService _contabilizarArquivoService;

        public ArquivosContabilizadosController(IContabilizarArquivoService contabilizarArquivoService) =>
            _contabilizarArquivoService = contabilizarArquivoService;

        [HttpGet]
        public ArquivosResponseDTO BuscarTodasAplicacoesContabilizadas()
        {
            ArquivosResponseDTO arquivosResponseDTO = new ArquivosResponseDTO();

            var estatisticas = _contabilizarArquivoService.EventosArmazenados.Select(x => new ArquivoEstatisticaDTO
            {
                NomeAplicacao = x.Value.NomeAplicacao,
                DataUltimaLeitura = x.Value.DataUltimaLeitura,
                NomeMaquina = x.Value.NomeMaquina,
                QuantidadeLeituras = x.Value.Quantidade
            }).ToList();

            arquivosResponseDTO.ArquivosLidos.AddRange(estatisticas);

            return arquivosResponseDTO;
        }
    }
}
