using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Libreria.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string from, string to, string subject, string body);
    }
}
