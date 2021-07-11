using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Persons.Dtos;

namespace TestCleanArch.Application.Persons.Queries
{
    public class GetPersonListQuery : IRequest<List<PersonDto>>
    {
    }

    public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQuery, List<PersonDto>>
    {
        protected readonly ITestCleanArchDbContext _context;
        protected readonly IMapper _mapper;
        public GetPersonListQueryHandler(ITestCleanArchDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<PersonDto>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Persons
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Id)
                .ToListAsync(cancellationToken);

            return products;
        }
    }


}
