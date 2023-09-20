using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.DTOs
{
    public class SerieDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? CantidadCapitulos { get; set; }
        public string? Url { get; set; }
        public List<CapituloDTO> Capitulos { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
