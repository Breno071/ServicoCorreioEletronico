using Consumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IConsumer, ClientConsumer>();

var host = builder.Build();
host.Run();
