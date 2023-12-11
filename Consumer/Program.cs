using AppRepository.Configuration;
using AppRepository.Data;
using ConsumerWindowsService;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

RepoContextConfig.AddDbContext(builder.Services, builder.Configuration);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
