using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.DTOs
{
    public class UpdateAbonadoStatusDTO
    {
        public int AbonadoId { get; set; }
        public string NuevoEstado { get; set; }
    }
}
