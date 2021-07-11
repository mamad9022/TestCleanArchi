using System;
using MediatR;

namespace TestCleanArch.Application.Persons.Command.UpdatePerson
{
    public class UpdatePersonCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
