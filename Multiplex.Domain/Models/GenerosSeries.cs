using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Models
{
    public partial class GenerosSeries
    {
        public int IdGn { get; set; }
        public int IdSr { get; set; }

        public virtual Generos IdGnNavigation { get; set; }
        public virtual Series IdSrNavigation { get; set; }
    }
}
