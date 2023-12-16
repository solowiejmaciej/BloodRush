#region

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        //TODO Change every task to use dapper
        services.AddTransient<IDbConnection>((sp) => new SqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString")));
    })
    .Build();

host.Run();