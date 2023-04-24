using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Models
{
    public partial class Generos
    {
        public Generos()
        {
            GenerosPeliculas = new HashSet<GenerosPeliculas>();
            GenerosSeries = new HashSet<GenerosSeries>();
        }

        public int IdGn { get; set; }
        public string DescripcionGn { get; set; }

        public virtual ICollection<GenerosPeliculas> GenerosPeliculas { get; set; }
        public virtual ICollection<GenerosSeries> GenerosSeries { get; set; }
    }
}
