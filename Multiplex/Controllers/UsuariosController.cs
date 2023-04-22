using Multiplex.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : BaseController
    {
        private readonly IUsuariosService usuariosService;

        public UsuariosController(IConfiguration configuration, IUsuariosService usuariosService) : base(configuration)
        {
            this.usuariosService = usuariosService;
        }
        [HttpGet("pendientes")]
        public async Task<IActionResult> GetAbonadosPendientes()
        {
            return Ok(await usuariosService.GetAbonadosPendientes());
        }
    }
}
