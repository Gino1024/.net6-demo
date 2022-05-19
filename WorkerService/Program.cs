using Coravel;
using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseSystemd()
    .ConfigureServices((hostContext,services) =>
    {
        services.AddHostedService<Worker>();
        #region Register Coravel
        //services.AddScheduler();
        //services.AddTransient<CoravelDemo>();
        #endregion
    })
    .Build();

#region Set Schedule Frequency
//host.Services.UseScheduler(scheduler =>
//{
//    scheduler.Schedule<CoravelDemo>()
//       .EverySecond();
//});
#endregion

await host.RunAsync();
