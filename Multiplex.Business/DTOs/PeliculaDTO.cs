using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplex.Business.DTOs
{
    public class PeliculaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Duracion { get; set; }
        public string Elenco { get; set; }
        public string Url { get; set; }
        public string Portada { get; set; }
        public List<GeneroDTO> Generos { get; set; }
        public List<int> GenerosList { get; set; }
        public IFormFile file { get; set; }
        public IFormFile portadaFile { get; set; }
    }
}
