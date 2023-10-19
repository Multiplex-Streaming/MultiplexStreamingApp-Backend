﻿using Microsoft.AspNetCore.Authorization;
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
    public class HistorialPeliculasController : BaseController
    {
        private readonly IHistorialPeliculasService historialPeliculasService;

        public HistorialPeliculasController(IConfiguration configuration, IHistorialPeliculasService historialPeliculasService) : base(configuration)
        {
            this.historialPeliculasService = historialPeliculasService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveHistorialPelicula([FromBody] HistorialPeliculasDTO historialPelicula) =>
            Ok(await historialPeliculasService.CreateHistorialPelicula(historialPelicula));

        [HttpGet("{idUsr}")]
        public async Task<IActionResult> GetHistorialPeliculas([FromRoute] int idUsr) =>
            Ok(await historialPeliculasService.GetHistorialPeliculas(idUsr));

        [HttpDelete("{idUsr}/{movieId}")]
        public async Task<IActionResult> DeleteHistorialPelicula([FromBody] HistorialPeliculasDTO historialPelicula) =>
            Ok(await historialPeliculasService.DeleteHistorialPelicula(historialPelicula));

        [HttpGet("recomendaciones/{idUsr}")]
        public async Task<IActionResult> GetRecomendaciones([FromRoute] int idUsr) =>
            Ok(await historialPeliculasService.GetRecomendaciones(idUsr));
    }
}
