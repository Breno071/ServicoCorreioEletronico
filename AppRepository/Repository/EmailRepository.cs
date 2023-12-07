using AppRepository.Interfaces;
using Utills.Models;

namespace AppRepository.Repository
{
    public class EmailRepository : IEmailRepository
    {
        public Task<List<Email>> GetNotProcessedEmails()
        {
            throw new NotImplementedException();
            //Buscar na base os emails que precisam ser enviados
        }
    }
}
