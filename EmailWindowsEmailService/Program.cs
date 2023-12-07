using AppRepository.Data;
using EmailWindowsEmailService;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContextPool<ApplicationContext>(options =>
{
    options.UseInMemoryDatabase("db");
});
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
