using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Xml;
using KrisnaldoApp.XMLMatch;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;

namespace MvcMovie.Controllers
{

    public class ErrorController : Controller
    {
        // GET: /<controller>/
        [HttpGet("/Error/{error}")]
        public IActionResult Index(int error)
        {
            return View(error);
        }
    }
}
