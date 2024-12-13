using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Models.Requests;

namespace Unicam.Libreria.Application.Validators
{
    public class AddLibroRequestValidator : AbstractValidator<AddLibroRequest>
    {
        public AddLibroRequestValidator()
        {
            RuleFor(x => x.Isbn)
                .NotEmpty()
                .WithMessage("Il campo isbn è obbligatorio")
                .NotNull()
                .WithMessage("Il campo isbn non può essere nullo");

            RuleFor(x=>x.Titolo)
                 .NotEmpty()
                .WithMessage("Il campo titolo è obbligatorio")
                .NotNull()
                .WithMessage("Il campo titolo non può essere nullo");

            RuleFor(x => x.Descrizione)
                .NotEmpty()
               .WithMessage("Il campo descrizione è obbligatorio")
               .NotNull()
               .WithMessage("Il campo descrizione non può essere nullo");

            RuleFor(x => x.IdAutore)
                .Custom(CheckAutoreExistance);

        }

        private void CheckAutoreExistance(int idAutore, ValidationContext<AddLibroRequest> context)
        {
           if (idAutore > 5)
            {
                context.AddFailure("Il campo id autore non può essere superiore a 5");
            } 
        }
    }
}
