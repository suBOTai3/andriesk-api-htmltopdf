using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace andriesk_api_htmltopdf.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        [HttpGet("api/heartbeat")]
        [Produces("application/json")]
        public IActionResult HeartBeat()
        {
            return StatusCode(200, new { status = "Alive and well" });
        }
    }
}
