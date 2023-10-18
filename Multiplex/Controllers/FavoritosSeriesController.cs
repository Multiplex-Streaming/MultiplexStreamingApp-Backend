using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FavoritosSeriesController : BaseController
    {
        private readonly IFavoritosSeriesService favoritosSeriesService;

        public FavoritosSeriesController(IConfiguration configuration, IFavoritosSeriesService favoritosSeriesService) : base(configuration)
        {
            this.favoritosSeriesService = favoritosSeriesService;
        }

        [HttpPost]
        public IActionResult SaveFavoritosSeries([FromBody] FavoritosSeriesDTO favoritosSeries) =>
            Ok(favoritosSeriesService.CreateFavoritosSeries(favoritosSeries));

        [HttpDelete]
        public IActionResult DeleteFavoritosSeries([FromBody] FavoritosSeriesDTO favoritosSeries) =>
            Ok(favoritosSeriesService.DeleteFavoritosSeries(favoritosSeries));

        [HttpGet]
        public IActionResult GetFavoritosSeries([FromQuery] int idUsr) =>
            Ok(favoritosSeriesService.GetFavoritosSeries(idUsr));
    }
}
