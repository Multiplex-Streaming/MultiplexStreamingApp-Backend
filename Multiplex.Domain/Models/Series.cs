using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Models
{
    public partial class Series
    {
        public Series()
        {
            CapituloSerie = new HashSet<CapituloSerie>();
            FavoritosSeries = new HashSet<FavoritosSeries>();
            HistorialSerie = new HashSet<HistorialSerie>();
            Relation89 = new HashSet<Relation89>();
        }

        public int IdSr { get; set; }
        public string NombreSr { get; set; }
        public string DescripcionSr { get; set; }
        public int? CantCapitulosSr { get; set; }

        public virtual ICollection<CapituloSerie> CapituloSerie { get; set; }
        public virtual ICollection<FavoritosSeries> FavoritosSeries { get; set; }
        public virtual ICollection<HistorialSerie> HistorialSerie { get; set; }
        public virtual ICollection<Relation89> Relation89 { get; set; }
    }
}
