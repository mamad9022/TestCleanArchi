using System.Threading.Tasks;

namespace TestCleanArch.Common.Interface
{
   public interface IMessageSendService
    {
        Task Send(MessageRequest mailRequest);
    }
}
