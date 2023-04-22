using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Multiplex.Business.Interfaces;
using Multiplex.Business.Services;
using System.Threading.Tasks;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaxonomyController : BaseController
    {
        private readonly ITaxonomyService taxonomyService;

        public TaxonomyController(IConfiguration configuration, ITaxonomyService taxonomyService) : base(configuration)
        {
            this.taxonomyService = taxonomyService;
        }

        [HttpGet("generos/{id}")]
        public async Task<IActionResult> GetPeliculasPorGenero([FromRoute]int id)
        {
            return Ok(await taxonomyService.GetPeliculasPorGenero(id));
        }
    }
}
