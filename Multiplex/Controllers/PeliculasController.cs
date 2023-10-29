using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Multiplex.Business.DTOs;
using Multiplex.Business.Interfaces;
using System.IO;
using System;
using System.Threading.Tasks;

namespace Multiplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeliculasController : BaseController
    {
        private readonly IPeliculasService peliculasService;

        public PeliculasController(IConfiguration configuration, IPeliculasService peliculasService) : base(configuration)
        {
            this.peliculasService = peliculasService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetPeliculas() => Ok(await peliculasService.GetPeliculas());
        [HttpGet("por-genero/{id}")]
        public async Task<IActionResult> GetPeliculasPorGenero([FromRoute] int id) => Ok(await peliculasService.GetPeliculasPorGenero(id));
        [HttpPost]
        [RequestSizeLimit(524_288_000)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SavePelicula([FromForm] PeliculaDTO pelicula) =>
            Ok(await peliculasService.CreatePelicula(pelicula));
        [HttpGet("{plId}")]
        public async Task<IActionResult> GetPelicula(int plId) =>
            Ok(await peliculasService.GetPelicula(plId));
        [HttpDelete("{plId}")]
        public async Task<IActionResult> DeletePelicula([FromRoute] int plId) =>
            Ok(await peliculasService.DeletePelicula(plId));
        [HttpPut]
        public async Task<IActionResult> UpdatePelicula([FromForm] PeliculaDTO pelicula) =>
            Ok(await peliculasService.UpdatePelicula(pelicula));

        [HttpGet("portada/{plId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPortada([FromRoute] int plId)
        {
            try
            {
                FileStream fileStream = await peliculasService.GetPeliculaPortada(plId);
                string contentType = "application/octet-stream";
                return File(fileStream, contentType, $"movie{plId}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("descargar/{idPl}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilePelicula([FromRoute] int idPl)
        {
            try
            {
                var fileStream = await peliculasService.GetPeliculaFile(idPl);

                // Determinar el tipo de contenido según el archivo (por ejemplo, "video/mp4")
                var contentType = "video/mp4";

                return File(fileStream, contentType, Path.GetFileName(idPl.ToString()));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
