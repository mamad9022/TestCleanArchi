using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestCleanArch.Application.Common.Exceptions;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Persons.Dtos;

namespace TestCleanArch.Application.Persons.Queries
{
    public class GetPersonDetailQuery : IRequest<PersonDto>
    {
        public Guid Id { get; set; }
    }

    public class GetPersonDetailQueryHandler:IRequestHandler<GetPersonDetailQuery, PersonDto>
    {
        protected readonly ITestCleanArchDbContext _context;
        protected readonly IMapper _mapper;
        public GetPersonDetailQueryHandler(ITestCleanArchDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonDto> Handle(GetPersonDetailQuery request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == request.Id);
            if (person is null)
                throw new NotFoundException(nameof(PersonDto), request.Id);
            return person;
        }
    }
}
