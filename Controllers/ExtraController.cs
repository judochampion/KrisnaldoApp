using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Xml;
using KrisnaldoApp.XMLMatch;
using System.Threading.Tasks;
using KrisnaldoApp.Models;
using Microsoft.AspNetCore.Builder;
using KrisnaldoApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace MvcMovie.Controllers
{
    public class ExtraController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public ExtraController(ApplicationDbContext context,IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public IActionResult AzureAccount()
        {
            return View();
        }

        public IActionResult DeployOnSmarterASP()
        {
            return View();
        }

        public IActionResult TestEensException()
        {
            throw new Exception();
        }

        public IActionResult NotFoundRonaldo()
        {
            Response.StatusCode = 404;
            return View();
        }


        public IActionResult RelationeleDatabase()
        {
            return View();
        }
        public IActionResult AngularJSDemo()
        {
            return View();
        }

        [Authorize]
        public IActionResult Refresh()
        {
           
            try
            {
                ViewBag.Status = "Het is gelukt om een refresh te doen van de data. Eerst werden alle data-tabellen leeggemaakt,"+User.Identity.Name+" en er zijn in totaal "+ SeedData.InitializeBis(_context, _hostingEnvironment)+ " rijen opnieuw toegevoegd aan de verschillende tabellen in de database.";
            }
            catch
            {
                ViewBag.Status = "Het is niet gelukt.";
            }
            return View();
        }


        public async Task<IActionResult> MailGenerator()
        {
            return View("Voorbeeldmail");
        }
    }
}
