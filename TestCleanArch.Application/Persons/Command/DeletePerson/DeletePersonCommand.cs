using System;
using MediatR;

namespace TestCleanArch.Application.Persons.Command.DeletePerson
{
    public class DeletePersonCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
