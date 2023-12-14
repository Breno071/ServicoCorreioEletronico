using AppRepository.Data;
using AppRepository.Entities;
using AppRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppRepository.Repository
{
    public class AdoptionRepository : IAdoptionRepository
    {
        private readonly ApplicationContext _context;

        public AdoptionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task BulkInsert(List<AdoptRequest> adoptions)
        {
            await _context.AddRangeAsync(adoptions);
            await _context.SaveChangesAsync();
        }

        public void Insert(AdoptRequest request)
        {
            _context.Adoptions.Add(request);
            _context.SaveChanges();
        }

        public async Task InsertAsync(AdoptRequest request)
        {
            await _context.Adoptions.AddAsync(request);
            await _context.SaveChangesAsync();
        }
    }
}
