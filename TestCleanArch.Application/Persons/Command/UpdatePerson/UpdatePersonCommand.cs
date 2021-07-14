using System;
using MediatR;
using TestCleanArch.Common.Result;

namespace TestCleanArch.Application.Persons.Command.UpdatePerson
{
    public class UpdatePersonCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
