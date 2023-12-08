﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Utills.Models;

namespace AppRepository.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<PendentEmail> PendentEmails { get; set; } = default!;
        public DbSet<Email> Emails { get; set; } = default!;
        public DbSet<Log> Logs { get; set; } = default!;
    }
}
