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
            try
            {
                await _context.AddRangeAsync(adoptions);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Insert(AdoptRequest request)
        {
            try
            {
                _context.Adoptions.Add(request);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task InsertAsync(AdoptRequest request)
        {
            try
            {
                await _context.Adoptions.AddAsync(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
