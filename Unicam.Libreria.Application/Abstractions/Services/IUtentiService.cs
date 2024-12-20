using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Application.Abstractions.Services
{
    public interface IUtentiService
    {
        Task<Utente> GetUtenteByEmailAsync(string email);
    }
}
