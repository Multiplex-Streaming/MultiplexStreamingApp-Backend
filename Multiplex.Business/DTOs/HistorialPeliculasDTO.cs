using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.DTOs
{
    public class HistorialPeliculasDTO
    {
        public int IdPl { get; set; }
        public int IdUsr { get; set; }
        public short Minutos { get; set; }
        public short Segundos { get; set; }
    }
}
