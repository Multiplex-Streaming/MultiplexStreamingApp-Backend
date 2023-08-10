using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.DTOs
{
    public class CapituloDTO
    {
        public int IdSr { get; set; }
        public int IdCp { get; set; }
        public string NombreCp { get; set; }
        public string DescripcionCp { get; set; }
        public string DuracionCp { get; set; }
        public string UrlCp { get; set; }
    }
}
