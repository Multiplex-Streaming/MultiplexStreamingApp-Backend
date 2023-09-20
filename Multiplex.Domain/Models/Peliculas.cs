using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Models
{
    public partial class Peliculas
    {
        public Peliculas()
        {
            FavoritosPelicula = new HashSet<FavoritosPelicula>();
            GenerosPeliculas = new HashSet<GenerosPeliculas>();
            HistorialPeliculas = new HashSet<HistorialPeliculas>();
        }

        public int IdPl { get; set; }
        public string TituloPl { get; set; }
        public string DescripcionPl { get; set; }
        public string DuracionPl { get; set; }
        public string ElencoPl { get; set; }
        public string UrlPl { get; set; }
        public string PortadaPl { get; set; }

        public virtual ICollection<FavoritosPelicula> FavoritosPelicula { get; set; }
        public virtual ICollection<GenerosPeliculas> GenerosPeliculas { get; set; }
        public virtual ICollection<HistorialPeliculas> HistorialPeliculas { get; set; }
    }
}
