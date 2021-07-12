using System;
using System.Threading.Tasks;
using TestCleanArch.Common.Interface;

namespace TestCleanArch.Common.Services
{
    public  class SmsService : IMessageSendService
    {
        public async Task Send(MessageRequest mailRequest)
        {
            Console.Write($"send to {mailRequest.To}");
        }
    }
}
