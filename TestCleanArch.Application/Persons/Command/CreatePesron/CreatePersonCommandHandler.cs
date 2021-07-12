using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Hangfire.Common;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Common.RabbitMq;
using TestCleanArch.Application.Persons.Dtos;
using TestCleanArch.Common;
using TestCleanArch.Common.Interface;
using TestCleanArch.Common.Services;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Persons.Command.CreatePesron
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDto>
    {
        protected readonly ITestCleanArchDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly IRecurringJobManager _recurringJobManager;
        protected readonly IMessageSendService _messageSend;
        private readonly IBusPublish _busPublish;
        private readonly MailSettings _mailSettings;


        public CreatePersonCommandHandler(ITestCleanArchDbContext context, IMapper mapper, IMessageSendService messageSend, IRecurringJobManager recurringJobManager, IBusPublish busPublish, IOptions<MailSettings> mailSettings)
        {
            _context = context;
            _mapper = mapper;
            _recurringJobManager = recurringJobManager;
            _messageSend = messageSend;
            _busPublish = busPublish;
            _mailSettings = mailSettings.Value;
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
                Username = request.Email,
                SendType = request.SendType
            };

            var person = _mapper.Map<Person>(entity);

            await _context.Persons.AddAsync(person);
            await _context.SaveAsync(cancellationToken);

            _recurringJobManager.AddOrUpdate("some-id", Job.FromExpression(() => Calculator(request.SendType,
                new MessageRequest
                {
                    Subject = "test",
                    Title = "text",
                    To = request.SendType ? request.Email : "09036139022"
                })), Cron.Minutely()); ;

            _busPublish.Send("creatuser",
                  JsonSerializer.Serialize(_mapper.Map<PersonDto>(person)));

            return _mapper.Map<PersonDto>(person);
        }

        public void Calculator(bool type, MessageRequest messageRequest)
        {
            if (type)
            {
                var stratgy = new MessageSendService(new EmailService(_mailSettings));
                 stratgy.Send(messageRequest).Wait();
            }
            else
            {
                var stratgy = new MessageSendService(new SmsService());
                 stratgy.Send(messageRequest).Wait();
            }
        }
    }
}