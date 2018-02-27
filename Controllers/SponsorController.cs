using KrisnaldoApp.Data;
using KrisnaldoApp.SponsorViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
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
            string webRootPath = _env.WebRootPath;
            //Read the contents of CSV file.  
            string csvData = System.IO.File.ReadAllText(webRootPath + @"\seed\sponsors\sponsorinfo.csv");

            List<Sponsor> sponsorlijst = new List<Sponsor>();

            //Execute a loop over the rows.  
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    string[] csvRow = row.Split(',');
                    SponsorViewModels.Sponsor newSponsor = new Sponsor();
                    newSponsor.DisplayNaam = csvRow[0];
                    newSponsor.RuwePictureNaam = csvRow[1];
                    newSponsor.Link = csvRow[2];
                    sponsorlijst.Add(newSponsor);
                }
            }
            return View(sponsorlijst);
        }

        [Authorize]
        public ActionResult RuweSponsors()
        {
            string webRootPath = _env.WebRootPath;
            //Read the contents of CSV file.  
            string csvData = System.IO.File.ReadAllText(webRootPath + @"\seed\sponsors\sponsorinfo.csv");

            List<Sponsor> sponsorlijst = new List<Sponsor>();

            //Execute a loop over the rows.  
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    string[] csvRow = row.Split(',');
                    SponsorViewModels.Sponsor newSponsor = new Sponsor();
                    newSponsor.DisplayNaam = csvRow[0];
                    newSponsor.RuwePictureNaam = csvRow[1];
                    newSponsor.Link = csvRow[2];
                    sponsorlijst.Add(newSponsor);
                }
            }
            return View(sponsorlijst);

        }

        [Authorize]
        public ActionResult DeleteSponsor(int id)
        {            
            string webRootPath = _env.WebRootPath;
            string fullPath = webRootPath + @"\seed\sponsors\sponsorinfo.csv";
            //Read the contents of CSV file.  
            string sCurrentText = System.IO.File.ReadAllText(fullPath).TrimEnd('\n');
            List<string> listOfLines = sCurrentText.Split(new char[] { '\n'}).ToList<String>();
            if (id > listOfLines.Count|| id <1) return Content("Id was te hoog of te laag.");
            listOfLines.RemoveAt(id-1);
            listOfLines.RemoveAll(s => s==null || s==String.Empty || s=="");
            string sNewText = String.Join("\n", listOfLines);
            System.IO.File.WriteAllText(fullPath, sNewText);
            return RedirectToAction("RuweSponsors");
        }

        [Authorize]
        [HttpGet]

        public ActionResult UploadFile()
        {
            ViewBag.Message = "Nog geen files  upgeload.";
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult UploadFile(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');

                string webRootPath = _env.WebRootPath;

                string _path = Path.Combine(webRootPath + @"\seed\sponsors\compressed\",filename);
                filename = _path;
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            ViewBag.Message = files.Count()+@"file(s)/"+size+"bytes uploaded successfully!";
            return View();
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
        [HttpGet]
        public async Task<IActionResult> AddSponsor()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddSponsor(string displaynaam, string rawname, string link)
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
            return RedirectToAction("RuweSponsors");
        }
    }
}
