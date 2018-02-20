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
        

            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var match = await _context.Match
                .Include(m=>m.Paragrafen)
                .Include(m=>m.SpelerMatchen)
                .ThenInclude(s=>s.Speler)
                .Include(m=>m.Goals)  
                .ThenInclude(g=>g.ScorerSpeler)
                .Include(m=>m.Goals)
                .ThenInclude(g=>g.AssistSpeler)            
                .SingleOrDefaultAsync(m => m.MatchID == id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }
        /*
        public async Task<IActionResult> Statistieken()
        {
            return View(await _context.Match.ToListAsync());
        }*/
    }
}
