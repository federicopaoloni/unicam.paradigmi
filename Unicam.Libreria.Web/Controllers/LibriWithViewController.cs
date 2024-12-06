using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Infrastructure.Database;

namespace Unicam.Libreria.Web.Controllers
{
    public class LibriWithViewController : Controller
    {
        private readonly IMyDbContext _context;
        public LibriWithViewController(IMyDbContext context)
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
