using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/t/[action]")]
    public class TController : Controller
    {
        /// <summary>
        /// 删除项目的API
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IEnumerable<string> aa()
        {
            return new List<string>() { "yyy", "uuu" };
        }

        [HttpGet]
        public IEnumerable<string> bb()
        {
            return new List<string>() { "11111", "22222" };
        }
    }
}