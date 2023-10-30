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
    public class FavoritosPeliculaController : BaseController
    {
        private readonly IFavoritosPeliculaService favoritosPeliculaService;

        public FavoritosPeliculaController(IConfiguration configuration, IFavoritosPeliculaService favoritosPeliculaService) : base(configuration)
        {
            this.favoritosPeliculaService = favoritosPeliculaService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFavoritosPelicula([FromQuery] int peliculaId) =>
            Ok(await favoritosPeliculaService.CreateFavoritosPelicula(userId, peliculaId));

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoritosPelicula([FromQuery] int peliculaId) =>
            Ok(await favoritosPeliculaService.DeleteFavoritosPelicula(userId, peliculaId));

        [HttpGet]
        public async Task<IActionResult> GetFavoritosPelicula() =>
            Ok(await favoritosPeliculaService.GetFavoritosPelicula(userId));
    }
}
