using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace HelloWorldASPNET.Controllers
{
    [Route("p")]
    public class LBartosController: Controller
    {
        [Route("")]
        public IActionResult Pipec()
        {
            return Content("he he");
        }

        [Route("a")]
        public ObjectResult Tada()
        {
            var albarto = new AlBarto() {Location = "Brasil", Outcome = 12};
            return new ObjectResult(albarto);
        }

        [Route("h")]
        public ViewResult Hello()
        {
            var domingo = new AlBarto() { Location = "Brasil", Outcome = 12 };
            return View(domingo);
        }

        [Route("h2")]
        public ViewResult Supers()
        {
            var lBartos = new List<AlBarto>()
            {
                new AlBarto() {Location = "Brasil", Outcome = 12},
                new AlBarto() {Location = "Italy", Outcome = 12},
            };

            return View(lBartos);
        }
    }
}
