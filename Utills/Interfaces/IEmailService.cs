using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utills.Models;

namespace Utills.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(Email email);
    }
}
