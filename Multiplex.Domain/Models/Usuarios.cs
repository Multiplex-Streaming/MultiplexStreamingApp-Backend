using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            FavoritosPelicula = new HashSet<FavoritosPelicula>();
            FavoritosSeries = new HashSet<FavoritosSeries>();
            HistorialPeliculas = new HashSet<HistorialPeliculas>();
            HistorialSeries = new HashSet<HistorialSeries>();
        }

        public int IdUsr { get; set; }
        public int IdTc { get; set; }
        public int IdEc { get; set; }
        public string NombreUsr { get; set; }
        public string ApellidoUsr { get; set; }
        public string CorreoUsr { get; set; }
        public string PasswordUsr { get; set; }
        public DateTime FechaAltaUsr { get; set; }
        public DateTime FechaModificacionUsr { get; set; }
        public decimal VerificacionUsr { get; set; }

        public virtual EstadosCuentas IdEcNavigation { get; set; }
        public virtual TiposCuentas IdTcNavigation { get; set; }
        public virtual ICollection<FavoritosPelicula> FavoritosPelicula { get; set; }
        public virtual ICollection<FavoritosSeries> FavoritosSeries { get; set; }
        public virtual ICollection<HistorialPeliculas> HistorialPeliculas { get; set; }
        public virtual ICollection<HistorialSeries> HistorialSeries { get; set; }
        public virtual ICollection<UsuariosPagos> UsuariosPagos { get; set; }
    }
}
