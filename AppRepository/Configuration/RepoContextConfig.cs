using AppRepository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRepository.Configuration
{
    public class RepoContextConfig
    {
        public static void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("db"));
        }
    }
}
