using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Hangfire.Common;
using MediatR;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Persons.Dtos;
using TestCleanArch.Common;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Persons.Command.CreatePesron
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDto>
    {
        protected readonly ITestCleanArchDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly IRecurringJobManager _recurringJobManager;
        protected readonly IMailService _mailService;
        public CreatePersonCommandHandler(ITestCleanArchDbContext context, IMapper mapper, IMailService mailService, IRecurringJobManager recurringJobManager)
        {
            _context = context;
            _mapper = mapper;
            _recurringJobManager = recurringJobManager;
            _mailService = mailService;
        }
        public async Task<PersonDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {

            var entity = new Person
            {
                Id = Guid.NewGuid(),
                LastName = request.LastName,
                Email = request.Email,
                FirstName = request.FirstName,
                Password = "123456",
                Username = request.Email
            };

            var person = _mapper.Map<Person>(entity);

            await _context.Persons.AddAsync(person);
            await _context.SaveAsync(cancellationToken);

            _recurringJobManager.AddOrUpdate("some-id", Job.FromExpression(() => _mailService.SendEmailAsync(new MailRequest
            {
                Title = "test",
                Subject = "text",
                To = person.Email
            })), Cron.Minutely());


            return _mapper.Map<PersonDto>(person);
        }
    }
}