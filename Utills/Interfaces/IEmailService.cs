using AppRepository.Entities;

namespace Utills.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(Email email);
    }
}
