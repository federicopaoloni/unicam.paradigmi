using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Libreria.Application.Validators
{
    public class ValidateAutoreExistence<T> : PropertyValidator<T, int>
    {
        public override string Name => "CheckAutoreValidator";

        public override bool IsValid(ValidationContext<T> context, int idAutore)
        {
            if (idAutore > 5)
            {
                //context.AddFailure("Il campo id autore non può essere maggiore di 5");
                return false;
            }
            return true;
        }
        protected override string GetDefaultMessageTemplate(string errorCode)
        => "Il campo id autore non può essere maggiore di 5";
    }
}
