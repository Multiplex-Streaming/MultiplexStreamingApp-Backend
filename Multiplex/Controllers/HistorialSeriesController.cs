using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using System.Threading.Tasks;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class HistorialSeriesController : BaseController
    {
        private readonly IHistorialSeriesService historialSeriesService;

        public HistorialSeriesController(IConfiguration configuration, IHistorialSeriesService historialSeriesService) : base(configuration)
        {
            this.historialSeriesService = historialSeriesService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveHistorialSerie([FromBody] HistorialSeriesDTO historialSerie) =>
            Ok(await historialSeriesService.CreateHistorialSerie(historialSerie));

        [HttpPut]
        public async Task<IActionResult> UpdateHistorialSerie([FromBody] HistorialSeriesDTO historialSerie) =>
            Ok(await historialSeriesService.UpdateHistorialSerie(historialSerie));

        [HttpGet("{idUsr}")]
        public async Task<IActionResult> GetHistorialSeries([FromRoute] int idUsr) =>
            Ok(await historialSeriesService.GetHistorialSeries(idUsr));

        [HttpDelete("{idUsr}/{serieId}")]
        public async Task<IActionResult> DeleteHistorialSerie([FromBody] HistorialSeriesDTO historialSerie) =>
            Ok(await historialSeriesService.DeleteHistorialSerie(historialSerie));

        [HttpGet("recomendaciones/{idUsr}")]
        public async Task<IActionResult> GetRecomendaciones([FromRoute] int idUsr) =>
            Ok(await historialSeriesService.GetRecomendaciones(idUsr)); 
    }
}
