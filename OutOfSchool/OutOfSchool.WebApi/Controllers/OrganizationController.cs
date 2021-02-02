using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OutOfSchool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(ILogger<OrganizationController> logger)
        {
            _logger = logger;
        }

        public IActionResult TestOk()
        {
            return this.Ok();
        }

        [HttpGet("hello")]
        [Authorize]
        public IActionResult Hello()
        {
            return this.Ok("Hello world");
        }

    }
}
