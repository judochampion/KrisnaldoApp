using KrisnaldoApp.Data;
using KrisnaldoApp.Models;
using KrisnaldoApp.Models.StatistiekViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Controllers
{

    public class SeizoenenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeizoenenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seizoenen
        public async Task<IActionResult> Index()
        {

            var seizoenen = await _context.Seizoen
                .Include(s => s.Matchen)
                .ToListAsync(); 
            return View(seizoenen);
        }
        
        public async Task<IActionResult> CompetitieStatistieken(string seizoennaam)
        {
            CompetitieStatistiek vmStatistieken = new CompetitieStatistiek();
            //Het juiste seizoen ophalen
            Seizoen seizoen = await _context.Seizoen
            .Include(s => s.Matchen)
            .SingleOrDefaultAsync(s => s.SeizoenNaam.Equals(seizoennaam));
            vmStatistieken.Seizoen = seizoen;
            //Alle matchen van dat seizoen ophalen
            List <Match> compmatchen = (List <Match>)seizoen.Matchen.Where(m => m.Type == Models.Type.COMPETITIE).ToList();           
            vmStatistieken.AantalGespeeldeMatchen = compmatchen.Count();
            vmStatistieken.NogTeSpelenMatchen = 26 - vmStatistieken.AantalGespeeldeMatchen;
            double dPercentage = (double)vmStatistieken.AantalGespeeldeMatchen / (double)26*100;
            vmStatistieken.GespeeldPercentage = (int)Math.Round(dPercentage);
            vmStatistieken.AantalMatchenStyleWidthAttribute = CompetitieStatistiek.StyleWidthMaker(vmStatistieken.GespeeldPercentage);
            vmStatistieken.AantalMatchenGewonnen = 0;
            vmStatistieken.AantalMatchenGelijk = 0;
            vmStatistieken.AantalMatchenVerloren = 0;
            vmStatistieken.AantalGespeeldeMatchen = 0;
            vmStatistieken.AantalCleanSheets = 0;
            vmStatistieken.AantalGoalsGescoord = 0;
            vmStatistieken.AantalTegenGoalsGeIncasseerd = 0;
           
            HashSet<int> hsCompMatchID = new HashSet<int>();
            foreach(Match m in compmatchen)
            {
                vmStatistieken.AantalGespeeldeMatchen++;
                //HashSet van de MatchID's van de competitie bijhouden
                hsCompMatchID.Add(m.MatchID);
                vmStatistieken.AantalGoalsGescoord += m.OnzeScore;
                vmStatistieken.AantalTegenGoalsGeIncasseerd += m.HunScore;
                //Scores vergelijken
                if(m.OnzeScore>m.HunScore)
                {
                    vmStatistieken.AantalMatchenGewonnen++;
                }
                else if(m.OnzeScore==m.HunScore)
                {
                    vmStatistieken.AantalMatchenGelijk++;
                }
                else
                {
                    vmStatistieken.AantalMatchenVerloren++;
                }
                //Aantal Cleansheets
                if(m.HunScore==0)
                {
                    vmStatistieken.AantalCleanSheets++;
                }
            }
            //De percentages in de verdeelbalk uitrekenen
            vmStatistieken.AantalMatchenGewonnenStyleWidthAttribute = CompetitieStatistiek.StyleWidthMaker((int)Math.Round((double)vmStatistieken.AantalMatchenGewonnen / (double)vmStatistieken.AantalGespeeldeMatchen * 100));
            vmStatistieken.AantalMatchenGelijkStyleWidthAttribute = CompetitieStatistiek.StyleWidthMaker((int)Math.Round((double)vmStatistieken.AantalMatchenGelijk / (double)vmStatistieken.AantalGespeeldeMatchen * 100));
            vmStatistieken.AantalMatchenVerlorenStyleWidthAttribute = CompetitieStatistiek.StyleWidthMaker(100-((int)Math.Round((double)vmStatistieken.AantalMatchenGelijk / (double)vmStatistieken.AantalGespeeldeMatchen * 100))- ((int)Math.Round((double)vmStatistieken.AantalMatchenGewonnen / (double)vmStatistieken.AantalGespeeldeMatchen * 100)));

            //Statistieken Per Speler
            List<Speler> lsSpelerLijst = (List<Speler>)await _context.Speler.ToListAsync();
            SortedDictionary<int, CompetitieSeizoenStatistiekSpeler> spelerStatistieken = new SortedDictionary<int, CompetitieSeizoenStatistiekSpeler>();
            foreach(Speler speler in lsSpelerLijst)
            {
                CompetitieSeizoenStatistiekSpeler spss = new CompetitieSeizoenStatistiekSpeler();
                spss.speler = speler;
                spss.SpelerNaam = speler.VoorNaam + " " + speler.FamilieNaam;
                spss.AantalGeassisteerd = 0;
                spss.AantalGescoord = 0;
                spss.AantalGespeeld = 0;
                spelerStatistieken.Add(speler.SpelerID,spss);
            }
            //De goals en assists tellen waarvan de match tot de hashset van matchID's van deze competitie hoort
            List<Goal> lsGoalLijst = (List<Goal>)await _context.Goal.ToListAsync();
            foreach(Goal goal in lsGoalLijst)
            {
                if(hsCompMatchID.Contains(goal.MatchID))
                {

                    spelerStatistieken[goal.ScorerSpelerID].AantalGescoord++;
                    if (goal.AssistSpelerID != null)
                    {
                        spelerStatistieken[(int)goal.AssistSpelerID].AantalGeassisteerd++;
                    }
                }
            }
            //Hoeveel matchen heeft elke speler gespeeld in deze competitie?
            List<SpelerMatch> lsSpelerMatchenLijst = (List<SpelerMatch>)await _context.SpelerMatch.ToListAsync();
            foreach (SpelerMatch spelermatch in lsSpelerMatchenLijst)
            {
                if (hsCompMatchID.Contains(spelermatch.MatchID))
                {
                    spelerStatistieken[spelermatch.SpelerID].AantalGespeeld++;
                }
            }

            //Nu gaan we er dictionary's van maken die dan mee met het ViewModel naar de View mogen
            vmStatistieken.dAssistDic = new Dictionary<string, int>();
            vmStatistieken.dGespeeldDic = new Dictionary<string, int>();
            vmStatistieken.dScoorDic = new Dictionary<string, int>();

            foreach(KeyValuePair<int,CompetitieSeizoenStatistiekSpeler> entry in spelerStatistieken)
            {
                if(entry.Value.AantalGeassisteerd>0)
                {
                    vmStatistieken.dAssistDic.Add(entry.Value.SpelerNaam, entry.Value.AantalGeassisteerd);
                }
                if (entry.Value.AantalGescoord > 0)
                {
                    vmStatistieken.dScoorDic.Add(entry.Value.SpelerNaam, entry.Value.AantalGescoord);
                }
                if (entry.Value.AantalGespeeld > 0)
                {
                    vmStatistieken.dGespeeldDic.Add(entry.Value.SpelerNaam, entry.Value.AantalGespeeld);
                }
            }
            return View(vmStatistieken);
        }
        
    }
}
