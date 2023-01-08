using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MvcCore.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("api/test")]
        public IActionResult GetMessage()
        {
            return new JsonResult(
                new
                {
                    Name = "Andy",
                    Age = 30
                });
        }
    }
}