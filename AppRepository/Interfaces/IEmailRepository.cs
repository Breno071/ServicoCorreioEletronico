using AppRepository.Entities;
using Utills.Models;

namespace AppRepository.Interfaces
{
    public interface IEmailRepository
    {
        Task<List<PendentEmail>> GetNotProcessedEmails();
        Task Insert(PendentEmail email);
        Task BulkInsert(List<PendentEmail> emails);
        Task Update(PendentEmail email);
    }
}
