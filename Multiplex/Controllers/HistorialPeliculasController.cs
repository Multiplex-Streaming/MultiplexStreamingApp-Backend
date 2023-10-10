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
        public async Task<IActionResult> SaveHistorialPelicula([FromBody] HistorialPeliculasDTO historialPelicula) =>
            Ok(await historialPeliculasService.CreateHistorialPelicula(historialPelicula));

        [HttpPut]
        public async Task<IActionResult> UpdateHistorialPelicula([FromBody] HistorialPeliculasDTO historialPelicula) =>
            Ok(await historialPeliculasService.UpdateHistorialPelicula(historialPelicula));

        [HttpGet("{idUsr}")]
        public async Task<IActionResult> GetHistorialPeliculas([FromRoute] int idUsr) =>
            Ok(await historialPeliculasService.GetHistorialPeliculas(idUsr));
    }
}
