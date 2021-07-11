using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Persons.Command.UpdatePerson;

namespace TestCleanArch.Application.Common.Validator.Person
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        protected readonly ITestCleanArchDbContext _context;
        public UpdatePersonCommandValidator(ITestCleanArchDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id).MustAsync(ValidId).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email is required");
        }

        private async Task<bool> ValidId(Guid perdonId, CancellationToken cancellationToken)
        {
            if (!await _context.Persons.AnyAsync(x => x.Id == perdonId, cancellationToken))
                return false;

            return true;
        }
    }
}
