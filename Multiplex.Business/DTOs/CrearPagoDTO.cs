using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.DTOs
{
    public class CrearPagoDTO
    {
        public DateTime FechaPago { get; set; }
        public bool IsPagado { get; set; }
        public decimal Total { get; set; }
    }
}
