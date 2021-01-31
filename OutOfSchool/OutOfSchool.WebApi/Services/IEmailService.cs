using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutOfSchool.WebApi.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
