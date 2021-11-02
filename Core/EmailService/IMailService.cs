using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EmailService
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
