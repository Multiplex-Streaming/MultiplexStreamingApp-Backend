using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Models
{
    public partial class FavoritosSeries
    {
        public int IdFavsr { get; set; }
        public int IdSr { get; set; }
        public int IdUsr { get; set; }

        public virtual Series IdSrNavigation { get; set; }
        public virtual Usuarios IdUsrNavigation { get; set; }
    }
}
