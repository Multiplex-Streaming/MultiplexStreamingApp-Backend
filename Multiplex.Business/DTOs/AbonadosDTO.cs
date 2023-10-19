using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplex.Business.DTOs
{
    public class AbonadosDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FecAlta { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
    }
}
