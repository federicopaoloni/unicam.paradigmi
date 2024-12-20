using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Application.Models.Requests;

namespace Unicam.Libreria.Application.Validators
{
    public class EditLibroRequestValidator : AbstractValidator<EditLibroRequest>
    {
        private readonly IMyDbContext _context;
        public EditLibroRequestValidator(IMyDbContext context)
        {
            _context = context;
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
                .SetValidator(new ValidateAutoreExistence<EditLibroRequest>());
            RuleFor(x => x.Isbn)
             .SetValidator(new IsbnUniqueValidator<EditLibroRequest>(_context,IsbnUniqueValidatorMode.ALLOW_IN_THE_SAME_ENTITY));

        }

     
    }
}
