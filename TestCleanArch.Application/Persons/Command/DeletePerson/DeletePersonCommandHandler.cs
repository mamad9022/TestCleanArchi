using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestCleanArch.Application.Common.Exceptions;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Persons.Command.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Guid>
    {
        protected readonly ITestCleanArchDbContext _context;
        public DeletePersonCommandHandler(ITestCleanArchDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (person is null)
                throw new NotFoundException(nameof(Person), request.Id);

            _context.Persons.Remove(person);
            await _context.SaveAsync(cancellationToken);

            return person.Id;
        }
    }
}
