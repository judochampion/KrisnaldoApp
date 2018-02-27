using KrisnaldoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KrisnaldoApp.Controllers
{



    public class MatchenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matchen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Match.ToListAsync());
        }


        public async Task<IActionResult> DetailsViaID(int? matchID)
        {
            if (matchID == null)
            {
                return View("Index", await _context.Match.ToListAsync());
            }
            var match = await _context.Match
                .Include(m => m.Paragrafen)
                .Include(m => m.SpelerMatchen)
                .ThenInclude(s => s.Speler)
                .Include(m => m.Goals)
                .ThenInclude(g => g.ScorerSpeler)
                .Include(m => m.Goals)
                .ThenInclude(g => g.AssistSpeler)
                .SingleOrDefaultAsync(m => m.MatchID == matchID);
            if (match == null)
            {
                return NotFound();
            }
            return View("Details",match);
        }


        public async Task<IActionResult> DetailsViaNaam(string matchnaam)
        {
            if (matchnaam == null)
            {
                return View("Index", await _context.Match.ToListAsync());
            }
            int n;
            bool isNumeric = int.TryParse(matchnaam, out n);
            if (isNumeric)
            {
                return await DetailsViaID(n);
            }


            var match = await _context.Match
                .Include(m => m.Paragrafen)
                .Include(m => m.SpelerMatchen)
                .ThenInclude(s => s.Speler)
                .Include(m => m.Goals)
                .ThenInclude(g => g.ScorerSpeler)
                .Include(m => m.Goals)
                .ThenInclude(g => g.AssistSpeler)
                .SingleOrDefaultAsync(m => m.LinkNaam == matchnaam);

            if (match == null)
            {
                return NotFound();
            }
            return View("Details",match);
        }
    }
}
