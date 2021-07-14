using FluentValidation;
using TestCleanArch.Application.Persons.Command.CreatePesron;

namespace TestCleanArch.Application.Common.Validator.Person
{
    public  class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");
        }
    }
}
