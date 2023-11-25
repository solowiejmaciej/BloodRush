#region

using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

#endregion

namespace BloodRush.Tasks;

public class DeleteUnusedNotificationInfoTask
{
    private readonly ILogger _logger;

    public DeleteUnusedNotificationInfoTask(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DeleteUnusedNotificationInfoTask>();
    }

    [Function("DeleteUnusedNotificationInfoTask")]
    public void Run([TimerTrigger("* * * * *")] TimerInfo myTimer)
    {
        using var connection = new SqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
        connection.Open();
        
        var notificationInfoToDelete = GetNotificationInfoToDelete(connection);
        _logger.LogInformation($"Found {notificationInfoToDelete.Count} notification info to delete");
        if (notificationInfoToDelete.Count == 0) return;
        
        foreach (var notificationInfoId in notificationInfoToDelete)
        {
            DeleteNotificationInfo(notificationInfoId, connection);
        }
    }

    private void DeleteNotificationInfo(Guid notificationInfoId, SqlConnection connection)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM DonorsNotificationInfo WHERE Id = @id";
            command.Parameters.AddWithValue("@id", notificationInfoId);
            command.ExecuteNonQuery();
        }
    }


    private List<Guid> GetNotificationInfoToDelete(SqlConnection connection)
    {
        var notificationInfoToDelete = new List<Guid>();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Id FROM DonorsNotificationInfo Where DonorId not in (select Id from Donors)";
            

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) notificationInfoToDelete.Add(reader.GetGuid(0));
            }
        }

        return notificationInfoToDelete;
    }
}