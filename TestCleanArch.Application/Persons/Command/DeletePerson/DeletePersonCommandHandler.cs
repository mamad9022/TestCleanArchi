using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestCleanArch.Application.Common.Exceptions;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Common.Helper.Messages;
using TestCleanArch.Common.Result;

namespace TestCleanArch.Application.Persons.Command.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result>
    {
        protected readonly ITestCleanArchDbContext _context;
        protected readonly IMemoryCache _cache;
        
        public DeletePersonCommandHandler(ITestCleanArchDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (person is null)
                return Result.Failed(new NotFoundObjectResult
                    (new ApiMessage("موردی یافت نشد")));

            _context.Persons.Remove(person);
            await _context.SaveAsync(cancellationToken);
            _cache.Remove("Persons");
            return Result.SuccessFul();
        }
    }
}
