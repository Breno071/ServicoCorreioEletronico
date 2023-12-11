using AppRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRepository.Interfaces
{
    public interface ILogRepository
    {
        Task AddAsync(Log log);
        void Add(Log log);
    }
}
