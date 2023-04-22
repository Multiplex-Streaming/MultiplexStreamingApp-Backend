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
            Relation88 = new HashSet<Relation88>();
            Relation89 = new HashSet<Relation89>();
        }

        public int IdGn { get; set; }
        public string DescripcionGn { get; set; }

        public virtual ICollection<Relation88> Relation88 { get; set; }
        public virtual ICollection<Relation89> Relation89 { get; set; }
    }
}
