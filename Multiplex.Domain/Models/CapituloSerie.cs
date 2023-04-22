using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Models
{
    public partial class CapituloSerie
    {
        public int IdSr { get; set; }
        public int IdCp { get; set; }
        public string NombreCp { get; set; }
        public string DescripcionCp { get; set; }
        public string DuracionCp { get; set; }

        public virtual Series IdSrNavigation { get; set; }
    }
}
