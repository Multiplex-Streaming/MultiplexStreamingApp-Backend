using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Domain.Models
{
    public class Pagos
    {
        public int IdPago { get; set; }
        public DateTime FechaPago { get; set; }
        public bool IsPagado { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<UsuariosPagos> UsuariosPagos { get; set; }
    }
}
