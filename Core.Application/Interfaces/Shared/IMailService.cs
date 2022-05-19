using Signaturit.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace Signaturit.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}