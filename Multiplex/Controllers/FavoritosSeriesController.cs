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
        public async Task<IActionResult> SaveFavoritosSerie([FromQuery] int serieId) =>
            Ok(await favoritosSeriesService.CreateFavoritosSeries(userId, serieId));

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoritosSerie([FromQuery] int serieId) =>
            Ok(await favoritosSeriesService.DeleteFavoritosSeries(userId, serieId));

        [HttpGet]
        public async Task<IActionResult> GetFavoritosSerie() =>
            Ok(await favoritosSeriesService.GetFavoritosSeries(userId));

    }
}
