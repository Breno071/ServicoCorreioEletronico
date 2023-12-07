using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppRepository.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<PendentEmail> PendentEmails { get; set; } = default!;
    }
}
