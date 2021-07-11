using System.Threading.Tasks;
using TestCleanArch.Common;

namespace TestCleanArch.Application.Common.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
