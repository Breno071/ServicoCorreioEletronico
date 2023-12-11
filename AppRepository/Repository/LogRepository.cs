using AppRepository.Data;
using AppRepository.Entities;
using AppRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRepository.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationContext _context; // Injecao de dependenci

        public LogRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
        
        public void Add(Log log)
        {
            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}
