using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Abstractions.Email;
using Unicam.Libreria.Application.Options;

namespace Unicam.Libreria.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config, IOptions<AzureADOption> azureAdOption)
        {
            _config = config;
        }
        public async Task<bool> SendEmailAsync(string from, string to, string subject, string body)
        {
            
            return true;
        }
    }
}
