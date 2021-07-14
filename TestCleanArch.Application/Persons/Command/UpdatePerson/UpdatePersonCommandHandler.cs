using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Common.Helper.Messages;
using TestCleanArch.Common.Result;

namespace TestCleanArch.Application.Persons.Command.UpdatePerson
{
    public class UpdatePersonCommandHandler:IRequestHandler<UpdatePersonCommand, Result>
    {
        protected readonly ITestCleanArchDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly IMemoryCache _cache;


        public UpdatePersonCommandHandler(ITestCleanArchDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (person is null)
                return Result.Failed(new NotFoundObjectResult
                    (new ApiMessage("موردی یافت نشد")));
            _mapper.Map(request, person);
            await _context.SaveAsync(cancellationToken);
            _cache.Remove("Persons");
            return Result.SuccessFul();
        }
    }
}
