using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Libreria.Application.Options
{
    public class AzureADOption
    {
        public const string SECTION_NAME = "AzureAd";
        public string ClientId { get; set; } = string.Empty;
        public string TenantID { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;
        public string CallbackPath { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string CLientSecret { get; set; } = string.Empty;
         
    }
}
