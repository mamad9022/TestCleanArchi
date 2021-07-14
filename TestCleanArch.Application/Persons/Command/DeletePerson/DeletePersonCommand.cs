using System;
using MediatR;
using TestCleanArch.Common.Result;

namespace TestCleanArch.Application.Persons.Command.DeletePerson
{
    public class DeletePersonCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
