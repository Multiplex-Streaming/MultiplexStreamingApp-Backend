using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Multiplex.Business.DTOs;
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
        [HttpPost("generos")]
        public async Task<IActionResult> SaveGenero([FromBody] GeneroDTO genero) => Ok(await taxonomyService.SaveGenero(genero));
        [HttpGet("generos")]
        public async Task<IActionResult> GetGeneros() => Ok(await taxonomyService.GetGeneros());
    }
}
