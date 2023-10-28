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
    [Authorize]
    public class HistorialPeliculasController : BaseController
    {
        private readonly IHistorialPeliculasService historialPeliculasService;

        public HistorialPeliculasController(IConfiguration configuration, IHistorialPeliculasService historialPeliculasService) : base(configuration)
        {
            this.historialPeliculasService = historialPeliculasService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveHistorialPelicula([FromQuery] int peliculaId) =>
            Ok(await historialPeliculasService.CreateHistorialPelicula(userId, peliculaId));

        [HttpGet]
        public async Task<IActionResult> GetHistorialPeliculas() =>
            Ok(await historialPeliculasService.GetHistorialPeliculas(userId));

        [HttpDelete]
        public async Task<IActionResult> DeleteHistorialPelicula([FromQuery] int peliculaId) =>
            Ok(await historialPeliculasService.DeleteHistorialPelicula(peliculaId, userId));

        [HttpGet("recomendaciones")]
        public async Task<IActionResult> GetRecomendaciones() =>
            Ok(await historialPeliculasService.GetRecomendaciones(userId));

        [HttpPut]
        public async Task<IActionResult> UpdateHistorialPelicula([FromQuery] int peliculaId, [FromQuery] int minutos, [FromQuery] int segundos) =>
            Ok(await historialPeliculasService.UpdateHistorial(userId, peliculaId, minutos, segundos));

        [HttpGet("ultimo-guardado/{idPl}")]
        public async Task<IActionResult> GetHistorialPeliculas([FromRoute] int idPl) =>
            Ok(await historialPeliculasService.GetHistorialPelicula(userId, idPl));
    }
}
