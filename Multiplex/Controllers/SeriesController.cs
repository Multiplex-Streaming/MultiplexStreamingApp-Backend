using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using System.Threading.Tasks;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeriesController : Controller
    {
        private readonly ISeriesService seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            this.seriesService = seriesService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetSeries() => Ok(await seriesService.GetSeries());

        [HttpGet("{IdSr}")]
        public async Task<IActionResult> GetSerie(int IdSr) =>
            Ok(await seriesService.GetSerie(IdSr));

        [HttpPost]
        public async Task<IActionResult> SaveSerie([FromForm] SerieDTO serie) =>
            Ok(await seriesService.CreateSerie(serie));

        [HttpDelete("{IdSr}")]
        public async Task<IActionResult> DeleteSerie([FromRoute] int IdSr) =>
            Ok(await seriesService.DeleteSerie(IdSr));

        [HttpPut]
        public async Task<IActionResult> UpdateSerie([FromForm] SerieDTO serie) =>
            Ok(await seriesService.UpdateSerie(serie));

        [HttpGet("descargar/{url}")]
        public async Task<IActionResult> DescargarSerie(string url)
        {
            try
            {
                var fileStream = await seriesService.GetSerieFile(url);

                // Determinar el tipo de contenido según el archivo (por ejemplo, "video/mp4")
                var contentType = "application/octet-stream";

                return File(fileStream, contentType, url);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
