using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using Multiplex.Business.Services;
using System.IO;
using System;
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

        [HttpGet("portada/{srId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPortada([FromRoute] int srId)
        {
            try
            {
                FileStream fileStream = await seriesService.GetSeriePortada(srId);
                string contentType = "application/octet-stream";
                return File(fileStream, contentType, $"serie{srId}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /*
         * crear capitulo series/capitulo
         * delete cap serie/capitulo HttpDelete("{IdSr}"
         * HttpPut
         */

        //Crear capitulo
        [HttpPost ("capitulo")]
        public async Task<IActionResult> SaveCapitulo([FromForm] CapituloDTO capitulo) =>
            Ok(await seriesService.CreateCapitulo(capitulo));

        [HttpPut ("capitulo")]
        public async Task<IActionResult> UpdateCapitulo([FromForm] CapituloDTO capitulo) =>
            Ok(await seriesService.UpdateCapitulo(capitulo));

        [HttpDelete("capitulo/{cpId}")]
        public async Task<IActionResult> DeleteCapitulo([FromRoute] int cpId) =>
            Ok(await seriesService.DeleteCapitulo(cpId));

        [HttpGet("capitulo/portada/{cpId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCapituloPortada([FromRoute] int cpId)
        {
            try
            {
                FileStream fileStream = await seriesService.GetCapituloPortada(cpId);
                string contentType = "application/octet-stream";
                return File(fileStream, contentType, $"capitulo{cpId}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
