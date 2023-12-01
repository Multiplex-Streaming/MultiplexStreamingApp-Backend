using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.DTOs
{
    public class PagoDTO
    {
        public DateTime FechaPago { get; set; }
        public bool IsPagado { get; set; }
        public decimal Total { get; set; }
        public int IdUsuario { get; set; }
    }
}
