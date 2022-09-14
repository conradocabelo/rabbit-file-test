using Microsoft.AspNetCore.Mvc;
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
        public List<ArquivoEstatisticaEvento> BuscarTodasAplicacoesContabilizadas() => 
            _contabilizarArquivoService.EventosArmazenados.Values.ToList();
    }
}
