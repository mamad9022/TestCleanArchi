using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestCleanArch.Application.Common.Exceptions;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Persons.Command.UpdatePerson
{
    public class UpdatePersonCommandHandler:IRequestHandler<UpdatePersonCommand, Guid>
    {
        protected readonly ITestCleanArchDbContext _context;
        public UpdatePersonCommandHandler(ITestCleanArchDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (person is null)
                throw new NotFoundException(nameof(Person), request.Id);

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;

            await _context.SaveAsync(cancellationToken);

            return person.Id;
        }
    }
}
