using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KrisnaldoApp.Controllers
{
    public class UpdateDatabaseController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        /*public ActionResult UpdateDatabase()
        {
            if (ModelState.IsValid)
            {
                // do your stuff like: save to database and redirect to required page.

            }

            // If we got this far, something failed, redisplay form
            //return View(model);
        }*/



    }
}