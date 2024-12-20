using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Application.Abstractions.Services;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Application.Services
{
    public class UtentiService : IUtentiService
    {
        private IMyDbContext _context;
        public UtentiService(IMyDbContext context)
        {
            _context = context;
        }
        public async Task<Utente> GetUtenteByEmailAsync(string email)
        {
            var utente = await _context.Utenti
                .Include(i=>i.Ruolo)
                .Where(w => w.EmailUtente == email)
                .FirstOrDefaultAsync();
            if (utente == null)
            {
                throw new Exception($"Utente {email} non esistente");
            }
            return utente;
        }
    }
}
