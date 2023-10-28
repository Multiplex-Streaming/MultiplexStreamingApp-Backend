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
    public class FavoritosSeriesController : BaseController
    {
        private readonly IFavoritosSeriesService favoritosSeriesService;

        public FavoritosSeriesController(IConfiguration configuration, IFavoritosSeriesService favoritosSeriesService) : base(configuration)
        {
            this.favoritosSeriesService = favoritosSeriesService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFavoritosSerie([FromBody] FavoritosSeriesDTO favoritosSerie) =>
            Ok(await favoritosSeriesService.CreateFavoritosSeries(favoritosSerie));

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoritosSerie([FromBody] FavoritosSeriesDTO favoritosSerie) =>
            Ok(await favoritosSeriesService.DeleteFavoritosSeries(favoritosSerie));

        [HttpGet]
        public async Task<IActionResult> GetFavoritosSerie([FromQuery] int idUsr) =>
            Ok(await favoritosSeriesService.GetFavoritosSeries(idUsr));

    }
}
