using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Models
{
    public partial class GenerosPeliculas
    {
        public int IdGn { get; set; }
        public int IdPl { get; set; }

        public virtual Generos IdGnNavigation { get; set; }
        public virtual Peliculas IdPlNavigation { get; set; }
    }
}
