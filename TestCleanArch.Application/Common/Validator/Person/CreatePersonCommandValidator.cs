﻿using FluentValidation;
using TestCleanArch.Application.Persons.Command.CreatePesron;

namespace TestCleanArch.Application.Common.Validator.Person
{
    public  class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email is required");
        }
    }
}