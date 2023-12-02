using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Multiplex.Controllers
{
    public class BaseController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        protected int userId { get { return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); } }
        public BaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
