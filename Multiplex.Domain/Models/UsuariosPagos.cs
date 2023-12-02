using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Domain.Models
{
    public class UsuariosPagos
    {
        public int IdUsr { get; set; }
        public int IdPago { get; set; }
        public virtual Usuarios Usuario { get; set; }
        public virtual Pagos Pago { get; set; }
    }
}
