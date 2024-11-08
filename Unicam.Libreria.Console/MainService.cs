using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Core.Entities;
using Unicam.Libreria.Infrastructure.Database;

namespace Unicam.Libreria.Console
{
    public class MainService
    {
        private MyDbContext _context;
        public MainService(MyDbContext context)
        {
            _context = context;
        }

        private void JoinManuale()
        {
            var result = (from l in _context.Libri
                          join a in _context.Autori on l.IdAutore equals a.Id
                          select new Libro()
                          {
                              Id = l.Id,
                              Isbn = l.Isbn,
                              Autore = a,
                              IdAutore = l.IdAutore,
                              IdCategoria = l.IdCategoria,
                              Titolo = l.Titolo,
                              Descrizione = l.Descrizione
                          }).ToList();
        }

        private void EagerLoading()
        {
            var libriConAutori = _context.Libri
                          .AsNoTracking()
                          .Include(i => i.Autore)
                          .ToList();
        }

        private async Task LazyLoadingAsync()
        {
            var libri = await _context.Libri
                .AsNoTracking()
                .ToListAsync();
            foreach (var libro in libri)
            {
                System.Console.WriteLine(libro.Autore.Nome);
            }
        }

        private async Task ExplicitLoadingAsync()
        {
            var libri = await _context.Libri
                .AsNoTracking()
                .ToListAsync();
            foreach(var libro in libri)
            {
                _context.Entry(libro).Reference(x => x.Autore).Load();
            }

        }
        public async Task ExecuteAsync()
        {
            /*var query = _context.Libri
                .Where(w => w.IdAutore == 1);

            query = query.Where(w => w.IdCategoria == 2);

            query.ToList();*/

            await LazyLoadingAsync();
            await ExplicitLoadingAsync();

            await AggiungiLibroAsync();
            await _context.SaveChangesAsync();

            /*
            var autori = _context.Autori.AsNoTracking().ToList();
            var categorie = _context.Categorie.AsNoTracking().ToList();
            var libri = _context.Libri.AsNoTracking().ToList();

            var libroDaModificare = libri.Where(w => w.Id == 2).First();

            
            AggiungiLibro();
            EditLibroGiaTracciato(libroDaModificare);
            EditDiAlcuneProprieta();
            DeleteLibro();
            _context.SaveChanges();
            */
            //System.Console.WriteLine("Stato dell'oggetto Nuovo Libro : " + _context.Entry(nuovoLibro).State);
            //System.Console.WriteLine("Stato dell'oggetto vecchio Libro : " + _context.Entry(oldLibro).State);
            /*
            nuovoLibro.Id = 1;

            _context.Libri.Add(nuovoLibro);*/

        }

        private void EditLibroGiaTracciato(Libro libro)
        {
            libro.Titolo = "TItolo Modificato";
        }

        private void EditDiAlcuneProprieta()
        {
            var libroDaModificare = new Libro();
            libroDaModificare.Id = 3;
            libroDaModificare.Isbn = "NUOVOISBN";
            libroDaModificare.Descrizione = "DESC";

            var entry = _context.Entry(libroDaModificare);
            entry.Property(p=>p.Isbn).IsModified = true;
            entry.Property(p => p.Descrizione).IsModified = true;
        }

        private void DeleteLibro()
        {
            var libroToDelete = new Libro();
            libroToDelete.Id = 4;

            _context.Entry(libroToDelete).State = EntityState.Deleted;
        }

        private async Task AggiungiLibroAsync()
        {
            var libro = new Libro();
            libro.IdAutore = 1;
            libro.IdCategoria = 1;
            libro.Titolo = "Aggiunta libro";
            libro.Descrizione = "Descrizione test";
            libro.Isbn = "123456";

            await _context.Libri.AddAsync(libro);
            //Il comando di add sul DbSet è analogo ad effettuare l'operazione qui sotto
            //_context.Entry(libro).State = EntityState.Added;
        }


    }
}
