using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NetCore2.MVC.BasicAuthentication.Controllers
{
    public class ValuesController : Controller
    {

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Query query)
        {

            var task = new Task(() =>
            {
               query.value = "OK";
            });

            task.Start();

            await task;

            return Ok(query);
        }

    }
}
