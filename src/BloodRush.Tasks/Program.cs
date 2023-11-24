#region

using Microsoft.Extensions.Hosting;

#endregion

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();