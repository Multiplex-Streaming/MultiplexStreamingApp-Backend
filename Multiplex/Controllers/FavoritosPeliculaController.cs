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
    public class FavoritosPeliculaController : BaseController
    {
        private readonly IFavoritosPeliculaService favoritosPeliculaService;

        public FavoritosPeliculaController(IConfiguration configuration, IFavoritosPeliculaService favoritosPeliculaService) : base(configuration)
        {
            this.favoritosPeliculaService = favoritosPeliculaService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFavoritosPelicula([FromBody] FavoritosPeliculaDTO favoritosPelicula) =>
            Ok(await favoritosPeliculaService.CreateFavoritosPelicula(favoritosPelicula));

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoritosPelicula([FromBody] FavoritosPeliculaDTO favoritosPelicula) =>
            Ok(await favoritosPeliculaService.DeleteFavoritosPelicula(favoritosPelicula));

        [HttpGet]
        public async Task<IActionResult> GetFavoritosPelicula([FromQuery] int idUsr) =>
            Ok(await favoritosPeliculaService.GetFavoritosPelicula(idUsr));
    }
}
