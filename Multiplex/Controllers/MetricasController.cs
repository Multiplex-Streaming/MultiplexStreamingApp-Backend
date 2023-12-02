using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Multiplex.Business.Interfaces;
using System.Threading.Tasks;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MetricasController : BaseController
    {
        private readonly IMetricasService metricasService;

        public MetricasController(IConfiguration configuration, IMetricasService metricasService) : base(configuration)
        {
            this.metricasService = metricasService;
        }

        [HttpGet("top-peliculas")]
        public async Task<IActionResult> GetTopPeliculas() =>
            Ok(await metricasService.GetTopPeliculas());

        [HttpGet("top-series")]
        public async Task<IActionResult> GetTopSeries() =>
            Ok(await metricasService.GetTopSeries());

        [HttpGet("generos-mas-vistos-peliculas")]
        public async Task<IActionResult> GetGenerosMasVistosPeliculas() =>
            Ok(await metricasService.GetGenerosMasVistosPeliculas());

        [HttpGet("generos-mas-vistos-series")]
        public async Task<IActionResult> GetGenerosMasVistosSeries() =>
            Ok(await metricasService.GetGenerosMasVistosSeries());

        [HttpGet("usuarios-mas-vieron-peliculas")]
        public async Task<IActionResult> GetUsuariosMasVieronPeliculas() =>
            Ok(await metricasService.GetUsuariosMasVieronPeliculas());

        [HttpGet("usuarios-mas-vieron-series")]
        public async Task<IActionResult> GetUsuariosMasVieronSeries() =>
            Ok(await metricasService.GetUsuariosMasVieronSeries());
    }
}
