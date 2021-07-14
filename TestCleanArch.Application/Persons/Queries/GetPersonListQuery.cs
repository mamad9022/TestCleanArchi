using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Persons.Dtos;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Persons.Queries
{
    public class GetPersonListQuery : IRequest<List<PersonDto>>
    {
    }

    public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQuery, List<PersonDto>>
    {
        protected readonly ITestCleanArchDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly IMemoryCache _cache;

        public GetPersonListQueryHandler(ITestCleanArchDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<List<PersonDto>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            if (!_cache.TryGetValue($"Persons", out List<Person> persons))
            {
                 persons = await GetPersons(cancellationToken);

                _cache.Set("Persons", persons,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(20)));
            }

            return _mapper.Map<List<PersonDto>>(persons);
        }


        private async Task<List<Person>> GetPersons(CancellationToken cancellationToken)
        {
            var query = await _context.Persons
                   .OrderBy(p => p.Id)
                   .ToListAsync(cancellationToken);

            _context.Dispose();

            return query;
        }
    }
}
