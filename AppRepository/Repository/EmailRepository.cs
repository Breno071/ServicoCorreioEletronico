using AppRepository.Data;
using AppRepository.Entities;
using AppRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppRepository.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationContext _context;

        public EmailRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task BulkInsert(List<PendentEmail> emails)
        {
            await _context.PendentEmails.AddRangeAsync(emails);
            await _context.SaveChangesAsync();
        }

        public async Task GetSenderEmail()
        {
            await _context.Emails.FirstAsync();
        }

        public async Task<List<PendentEmail>> GetNotProcessedEmails()
        {
            return await _context.PendentEmails.Where(x => x.Processed == false)
                .Include(x => x.Email)
                .ToListAsync();
        }

        public async Task Insert(PendentEmail email)
        {
            await _context.PendentEmails.AddAsync(email);
            await _context.SaveChangesAsync();
        }

        public async Task Update(PendentEmail email)
        {
            email.ProcessedDate = DateTime.Now;
            _context.Entry(email).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
