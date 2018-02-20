using KrisnaldoApp.Data;
using KrisnaldoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KrisnaldoApp.Controllers
{
    public class SpelersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpelersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Matchen
        public async Task<IActionResult> Index()
        {
          return View(await _context.Speler
              .Include(speler => speler.SpelerMatchen)
              .ThenInclude(spelermatch => spelermatch.Match)
              .Include(speler=>speler.Goals)
              .Include(speler=>speler.Assists)
              .ToListAsync());
        }

 
        public async Task<IActionResult> Details(string spelernaam)
        {
            
            if (spelernaam == null)
            {
                return View("Index", await _context.Speler
              .Include(sp => sp.SpelerMatchen)
              .ThenInclude(spelermatch => spelermatch.Match)      
              .Include(sp => sp.Goals)
              .Include(sp => sp.Assists)
              .ToListAsync());
                
            }

            var speler = await _context.Speler
                                .Include(s => s.SpelerMatchen)
              .ThenInclude(spelermatch => spelermatch.Match)

              .ThenInclude(matchke => matchke.Paragrafen)
              .Include(m => m.Goals)
              .Include(m => m.Assists)
              
              
                .SingleOrDefaultAsync(m => m.VoorNaam + m.FamilieNaam.Replace(" ","").ToLower() == spelernaam);


 

            //List<Match> listOfAllMatches = speler.SpelerMatchen.


            if (speler == null)
            {
                return NotFound();
            }
            

            KrisnaldoApp.Models.SpelerViewModels.SpelerIndividueelStatistiek sist = new Models.SpelerViewModels.SpelerIndividueelStatistiek();
            sist.Speler = speler;
            sist.AantalGespeeldeMatchen = speler.SpelerMatchen.Count;
            HashSet<int> hsSMIntegers = new HashSet<int>();
            sist.MatchenWaarHijHeeftAanMeegedaan = new List<Models.Match>();
            sist.QuotesUitVerslagen = new Dictionary<string, List<string>>();

            foreach (KrisnaldoApp.Models.SpelerMatch m in speler.SpelerMatchen)
            {
                sist.MatchenWaarHijHeeftAanMeegedaan.Add(m.Match);
            }
            foreach(Models.Match m in sist.MatchenWaarHijHeeftAanMeegedaan)
            {
                List<string> AlleZinnenUitEenMatch = new List<string>();
                foreach (Paragraaf p in m.Paragrafen)
                {
                    AlleZinnenUitEenMatch.AddRange(SplitSentences(p.Tekst));
                }
                List<string> AlleGefilterdeZinnenUitDieMatch = new List<string>();
                foreach (string z in AlleZinnenUitEenMatch)
                {

                    if (z.Contains(speler.VoorNaam))
                    {
                        AlleGefilterdeZinnenUitDieMatch.Add(z);
                    }

                }
                if (AlleGefilterdeZinnenUitDieMatch.Count > 0)
                {
                    
                    string sMatchBeschrijving = "uit het verslag van " + m.Datum.ToString("D", new System.Globalization.CultureInfo("nl-BE")) + " tegen " + m.Tegenstander;
                    sist.QuotesUitVerslagen.Add(sMatchBeschrijving, AlleGefilterdeZinnenUitDieMatch);
                }


            }
   

            return View(sist);
        }


        #region Best Sentence Parser

        /// <span class="code-SummaryComment"><summary></span>
        /// This is generally the most accurate approach to
        /// parsing a body of text into sentences to include
        /// the sentence's termination (e.g., the period,
        /// question mark, etc).  This approach will handle
        /// duplicate sentences with different terminations.
        ///
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="sSourceText"></param></span>
        /// <span class="code-SummaryComment"><returns></returns></span>
        private List<string> SplitSentences(string sSourceText)
        {
            // create a local string variable
            // set to contain the string passed it
            string sTemp = sSourceText;

            // create the array list that will    Regex.Split(sTemp, @"(?<=['""A-Za-z0-9][\.\!\?])\s+(?=[A-
            //Z])");
            // be used to hold the sentences
            List<string> al = new List<string>();
            char[] separators = new char[] { '!', '.', '?' };
            // split the sentences with a regular expression
            string[] splitSentences = Regex.Split(sTemp, @"(?<=[.?!])");
            

                /*
            Regex.Split(sTemp, @"[A-Z].*?([^\w\s]|(?=\n)|$)");*/

            // loop the sentences
            for (int i = 0; i < splitSentences.Length; i++)
            {
                // clean up the sentence one more time, trim it,
                // and add it to the array list
                string sSingleSentence =
                    splitSentences[i].Replace(Environment.NewLine,
                    string.Empty);
                al.Add(sSingleSentence.Trim());
            }

            // return the arraylist with
            // all sentences added
            return al;
        }

        #endregion

    }
}
