using AppRepository.Data;
using AppRepository.Entities;
using AppRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utills.Models;

namespace AppRepository.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationContext _context;

        public EmailRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<PendentEmail>> GetNotProcessedEmails()
        {
            return await _context.PendentEmails.Where(x => x.Processed == false).ToListAsync();
        }

        public async Task Update(PendentEmail email)
        {
            email.ProcessedDate = DateTime.Now;
            _context.Entry(email).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
