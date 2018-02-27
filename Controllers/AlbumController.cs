using KrisnaldoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Controllers
{

    public class AlbumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            System.Collections.Generic.List<Models.Album> list = await _context.Album.ToListAsync();
            list = list.OrderBy(a => a.Datum).ToList<Models.Album>();
            return View(list);
        }
       /* public async Task<IActionResult> Detailsi(nt? id)
        {
            if (id == null)
           { 
                return NotFound();
            }
            var album = await _context.Album
                .Include(a => a.Fotos)
                .SingleOrDefaultAsync(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }*/

        public async Task<IActionResult> Details(string albumnaam)
        {
            if(albumnaam == null)
            {

                System.Collections.Generic.List<Models.Album> list = await _context.Album.ToListAsync();
                list = list.OrderBy(a => a.Datum).ToList<Models.Album>();
                return View("Index",list);
            }

            var album = await _context.Album
                .Include(a => a.Fotos)
                .SingleOrDefaultAsync(a => a.RuweNaam == albumnaam);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        public async Task<IActionResult> Statistieken()
        {
            return View(await _context.Match.ToListAsync());
        }
    }
}
