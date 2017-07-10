using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWorldASPNET.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace HelloWorldASPNET.Controllers
{
    public class HomeController: Controller
    {
        public string Index()
        {
            return "Main Page Serving from MVC";
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            var allErrors = ModelState.Values.SelectMany(x => x.Errors);
            if (!ModelState.IsValid)
                return View(person);

            return RedirectToAction("Completed", new { message = "Created sucesfully"});
        }

        public IActionResult Completed(string message)
        {
            return View(message);
        }
    }
}
