using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Libreria.Core.Entities
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public string EmailUtente { get; set; } = string.Empty;
        public int IdRuolo { get; set; }
        public virtual Ruolo Ruolo { get; set; } = null!;
    }
}
