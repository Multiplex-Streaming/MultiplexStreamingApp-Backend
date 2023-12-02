using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using Multiplex.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PagosController : BaseController
    {
        private readonly IPagosService pagosService;

        public PagosController(IConfiguration configuration, IPagosService pagosService) : base(configuration)
        {
            this.pagosService = pagosService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPago([FromBody] PagoDTO pago)
        {
            if (pago == null)
            {
                return BadRequest("Invalid payload");
            }

            return Ok(await pagosService.AddPagoAsync(pago));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePago([FromRoute] int id, [FromBody] PagoDTO pago)
        {
            if (pago == null)
            {
                return BadRequest("Invalid payload");
            }

            return Ok(await pagosService.UpdatePagoAsync(id, pago));
        }

        [HttpGet("notificar-abonados-con-pagos-pendientes")]
        public async Task<IActionResult> NotificarAbonadosConPagosPendientes()
        {
            return Ok(await pagosService.NotificarAbonadosConPagosPendientes());
        }
    }
}
