using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResourceServer.Controllers
{
    [Route("identity")]
    [Authorize]
    //[ApiController]
    public class IdentityController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            //var s = User.Claims;
            return new JsonResult(User.Claims.Select(s => new { s.Type, s.Value }));
            //return new string[] { "value1", "value2" };
        }

    }
}
