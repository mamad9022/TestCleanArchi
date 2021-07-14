using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCleanArch.Application.Common.Exceptions;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Persons.Dtos;
using TestCleanArch.Common.Helper.Messages;
using TestCleanArch.Common.Result;

namespace TestCleanArch.Application.Persons.Queries
{
    public class GetPersonDetailQuery : IRequest<Result<PersonDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetPersonDetailQueryHandler:IRequestHandler<GetPersonDetailQuery, Result<PersonDto>>
    {
        protected readonly ITestCleanArchDbContext _context;
        protected readonly IMapper _mapper;
        public GetPersonDetailQueryHandler(ITestCleanArchDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<PersonDto>> Handle(GetPersonDetailQuery request, CancellationToken cancellationToken)
        {
                //.ProjectTo<PersonDto>(_mapper.ConfigurationProvider)

            var person = await _context.Persons
                .SingleOrDefaultAsync(x => x.Id == request.Id);
            if (person is null)
                return Result<PersonDto>.Failed(new NotFoundObjectResult
                     (new ApiMessage("موردی یافت نشد")));

            return Result<PersonDto>.SuccessFul(_mapper.Map<PersonDto>(person));
        }
    }
}
