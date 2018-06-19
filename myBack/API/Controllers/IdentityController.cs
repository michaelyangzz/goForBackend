using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Identity")]
    [Authorize]
    public class IdentityController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var retdata = from c in User.Claims select c.Type + ": " + c.Value;

            return retdata;
        }
    }
}