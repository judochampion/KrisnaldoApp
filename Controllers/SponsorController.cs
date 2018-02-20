using KrisnaldoApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Controllers
{

    public class SponsorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public SponsorController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Sponsors
        public async Task<IActionResult> Index()
        {
            System.Collections.Generic.List<Models.Sponsor> list = await _context.Sponsor.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Voorbeeldmail()
        {
            return View();
        }


        public async Task<IActionResult> MailGenerator()
        {
            return View("Voorbeeldmail");
        }

        [Authorize]
        public async Task<IActionResult> AddSponsor()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> FormSponsor(string displaynaam, string rawname, string link)
        {
            if (
                String.IsNullOrWhiteSpace(displaynaam) ||
                String.IsNullOrWhiteSpace(rawname) ||
                String.IsNullOrWhiteSpace(link))
                return Content("Invalid input");

            string webRootPath = _env.WebRootPath;
            string fullPath = webRootPath + @"\seed\sponsors\sponsorinfo.csv";
            //Read the contents of CSV file.  
            string sNewRecord = String.Join(",", new string[] { displaynaam, rawname, link });
            string sCurrentText = System.IO.File.ReadAllText(fullPath).TrimEnd('\r', '\n');

            if (
                sCurrentText.Contains(displaynaam) ||
                sCurrentText.Contains(rawname) ||
                sCurrentText.Contains(link))
                return Content("Duplicate input");

            string sNewText = sCurrentText + Environment.NewLine + sNewRecord;
            System.IO.File.WriteAllText(fullPath, sNewText);
            return Content(sNewText);
        }
    }
}
