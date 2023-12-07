using Utills.Models;

namespace AppRepository.Interfaces
{
    public interface IEmailRepository
    {
        Task<List<Email>> GetNotProcessedEmails();
    }
}
