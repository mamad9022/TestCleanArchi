using MediatR;
using TestCleanArch.Application.Persons.Dtos;
using TestCleanArch.Common.Result;

namespace TestCleanArch.Application.Persons.Command.CreatePesron
{
    public class CreatePersonCommand : IRequest<Result<PersonDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SendType { get; set; }

    }
}
