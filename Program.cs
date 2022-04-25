using Serilog;
using WebsiteChecker;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        //Logger configuration
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(hostContext.Configuration).CreateLogger();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();