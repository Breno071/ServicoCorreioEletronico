using AppRepository.Entities;
using AppRepository.Interfaces;
using Utills.Models;

namespace AppRepository.Repository
{
    public class EmailRepository : IEmailRepository
    {
        public Task<List<PendentEmail>> GetNotProcessedEmails()
        {
            throw new NotImplementedException();
            //Buscar na base os emails que precisam ser enviados
        }
    }
}
