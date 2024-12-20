using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Application.Models.Requests;

namespace Unicam.Libreria.Application.Validators
{
    public class IsbnUniqueValidator<T> : PropertyValidator<T, string>
    {
        private readonly IMyDbContext _context;
        private readonly IsbnUniqueValidatorMode _validatorMode;
        public IsbnUniqueValidator(IMyDbContext context, IsbnUniqueValidatorMode validatorMode )
        {
            _context = context;
            _validatorMode = validatorMode;
        }
        public override string Name => "IsbnUniqueValidator";

        public override bool IsValid(ValidationContext<T> context, string isbn)
        {
            var instance = context.InstanceToValidate;
            var query = _context.Libri.AsQueryable();
            query = query.Where(w => w.Isbn == isbn);
            if (_validatorMode == IsbnUniqueValidatorMode.ALLOW_IN_THE_SAME_ENTITY)
            {
                int idToCheck = 0;
                if (instance is EditLibroRequest editRequest)
                {
                    idToCheck = editRequest.Id;
                }
                query = query.Where(w=>w.Id!=idToCheck);
            }
            var numOfLibri = query.Count();
            if (numOfLibri > 0)
            {
                return false;
            }
            return true;
        }
        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "L'isbn {PropertyValue} è già presente all'interno della libreria";
        }
    }

    public enum IsbnUniqueValidatorMode
    {
        UNIQUE,
        ALLOW_IN_THE_SAME_ENTITY
    }
}
