using System.Threading.Tasks;
using TestCleanArch.Common.Interface;

namespace TestCleanArch.Common.Services
{
    public class MessageSendService
    {
        private readonly IMessageSendService _messageSend;
        public MessageSendService(IMessageSendService messageSend)
        {
            _messageSend = messageSend;
        }

        public async Task Send(MessageRequest messageRequest) => await _messageSend.Send(messageRequest);

    }
}
