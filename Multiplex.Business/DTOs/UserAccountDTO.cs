﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.DTOs
{
    public class UserAccountDTO
    {
        public int UserId { get; set; } 
        public bool EsAbonado { get; set; }
        public string Nombre { get; set; }  
        public string Apellido { get; set;}
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}
