using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Unicam.Libreria.Application.Models.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; }
    }
}
