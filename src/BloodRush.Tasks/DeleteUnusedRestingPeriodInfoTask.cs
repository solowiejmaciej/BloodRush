using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace BloodRush.Tasks;

public class DeleteUnusedRestingPeriodInfoTask
{
    private readonly ILogger _logger;

    public DeleteUnusedRestingPeriodInfoTask(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DeleteUnusedRestingPeriodInfoTask>();
    }

    [Function("DeleteUnusedRestingPeriodInfoTask")]
    public void Run([TimerTrigger("* * * * *")] TimerInfo myTimer)
    {
        using var connection = new SqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
        connection.Open();
        
        var restingPeriodInfoToDelete = GetRestingPeriodInfoToDelete(connection);
        _logger.LogInformation($"Found {restingPeriodInfoToDelete.Count} resting period info to delete");
        if (restingPeriodInfoToDelete.Count == 0) return;
        
        foreach (var restingPeriodInfo in restingPeriodInfoToDelete)
        {
            DeleteRestingPeriodInfo(restingPeriodInfo, connection);
        }
    }

    private void DeleteRestingPeriodInfo(Guid restingPeriodInfoId, SqlConnection connection)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM DonorsRestingPeriodInfo WHERE Id = @id";
            command.Parameters.AddWithValue("@id", restingPeriodInfoId);
            command.ExecuteNonQuery();
        }
    }


    private List<Guid> GetRestingPeriodInfoToDelete(SqlConnection connection)
    {
        var notificationInfoToDelete = new List<Guid>();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Id FROM DonorsRestingPeriodInfo Where DonorId not in (select Id from Donors)";
            

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) notificationInfoToDelete.Add(reader.GetGuid(0));
            }
        }

        return notificationInfoToDelete;
    }
}