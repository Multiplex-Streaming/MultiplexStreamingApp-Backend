using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiplex.Controllers
{
    public class BaseController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public BaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
