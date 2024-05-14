using Domain.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

}
