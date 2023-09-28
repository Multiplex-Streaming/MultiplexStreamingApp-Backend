using Multiplex.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Multiplex.Business.DTOs;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuariosController : BaseController
    {
        private readonly IUsuariosService usuariosService;

        public UsuariosController(IConfiguration configuration, IUsuariosService usuariosService) : base(configuration)
        {
            this.usuariosService = usuariosService;
        }

        [HttpGet("pendientes/{estado}")]
        public async Task<IActionResult> GetAbonadosPendientes([FromRoute] string estado) => Ok(await usuariosService.GetAbonadosPorEstado(estado));

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAccount([FromBody]UserAccountDTO userAccount) => Ok(await usuariosService.CreateUserAccount(userAccount));
        
        [HttpPost("change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword) => Ok(await usuariosService.ChangePassword(changePassword));

        [HttpPut("update-abonado-status")]
        public async Task<IActionResult> UpdateAbonadoStatus([FromBody] UpdateAbonadoStatusDTO updateAbonadoStatus) => Ok(await usuariosService.UpdateAbonadoStatus(updateAbonadoStatus.AbonadoId, updateAbonadoStatus.NuevoEstado));
    }
}
