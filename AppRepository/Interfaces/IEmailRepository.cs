using AppRepository.Entities;
using Utills.Models;

namespace AppRepository.Interfaces
{
    public interface IEmailRepository
    {
        Task<List<PendentEmail>> GetNotProcessedEmails();
    }
}
