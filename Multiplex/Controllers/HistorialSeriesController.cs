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
        public async Task<IActionResult> SaveHistorialSerie([FromQuery] int serieId) =>
            Ok(await historialSeriesService.CreateHistorialSerie(userId, serieId));

        [HttpGet]
        public async Task<IActionResult> GetHistorialSeries() =>
            Ok(await historialSeriesService.GetHistorialSeries(userId));

        [HttpDelete]
        public async Task<IActionResult> DeleteHistorialSerie([FromQuery] int serieId) =>
            Ok(await historialSeriesService.DeleteHistorialSerie(userId, serieId));

        [HttpGet("recomendaciones/{idUsr}")]
        public async Task<IActionResult> GetRecomendaciones([FromRoute] int idUsr) =>
            Ok(await historialSeriesService.GetRecomendaciones(idUsr)); 
    }
}
