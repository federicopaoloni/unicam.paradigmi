using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unicam.Libreria.Infrastructure.Database;

namespace Unicam.Libreria.Web.Controllers
{
    public class LibriController : Controller
    {
        private readonly MyDbContext _context;
        public LibriController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //TODO : Caricare tutti i libri presenti nel database
            //TODO : passare i libri alla view in modo da renderizzarli
            var libri = _context.Libri
                .Include(x=>x.Autore)
                .ToList();
            

            return View(libri);
        }
    }
}
