using MediatR;
using TestCleanArch.Application.Persons.Dtos;

namespace TestCleanArch.Application.Persons.Command.CreatePesron
{
    public class CreatePersonCommand : IRequest<PersonDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool SendType { get; set; }

    }
}
