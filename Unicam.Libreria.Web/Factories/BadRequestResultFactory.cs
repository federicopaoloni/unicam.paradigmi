using Microsoft.AspNetCore.Mvc;
using Unicam.Libreria.Application.Models.Responses;

namespace Unicam.Libreria.Web.Factories
{
    public class BadRequestResultFactory : BadRequestObjectResult
    {
        public BadRequestResultFactory(ActionContext context) : base(new BadResponse())
        {
            var ourResponse = (BadResponse)Value;
            ourResponse.Errors = new List<string>();
            foreach (var key in context.ModelState)
            {
                var errors = key.Value.Errors;
                foreach(var error in errors) {
                    ourResponse.Errors.Add(error.ErrorMessage);
                }
            }
        }
    }
}
