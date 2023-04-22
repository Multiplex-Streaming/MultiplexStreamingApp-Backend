using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplex.Business.DTOs
{
    public class UserInfoDTO
    {
        public int Id { get; set; } 
        public bool IsAdmin { get; set; }
        public string UserName { get; set; }
    }
}
