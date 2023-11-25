using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BloodRush.Tasks
{
    public class EndRestingPeriodTask
    {
        private readonly ILogger _logger;

        public EndRestingPeriodTask(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EndRestingPeriodTask>();
        }

        [Function("EndRestingPeriodTask")]
        public void Run([TimerTrigger("* * * * *")] TimerInfo myTimer, ExecutionContext context)
        {
            using var connection = new SqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
            connection.Open();

            var donors = GetDonorsWithActiveRestingPeriod(connection);
            _logger.LogInformation($"Found {donors.Count} donors with active resting period");
                
            if (donors.Count == 0) return;

            foreach (var donorId in donors)
            {
                var donorRestingInfo = GetDonorRestingPeriodEndDate(donorId, connection);
                _logger.LogInformation($"Resting period for donor {donorId} ends on {donorRestingInfo}");

                if (donorRestingInfo < DateTime.Today)
                {
                    _logger.LogInformation($"Resting period for donor {donorId} has ended");
                    UpdateRestingPeriodInfo(donorId, connection);
                }
                else
                {
                    _logger.LogInformation($"Resting period for donor {donorId} has not ended yet");
                }
            }
        }

        private void UpdateRestingPeriodInfo(Guid donorId, SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE DonorsRestingPeriodInfo SET IsRestingPeriodActive = 0 WHERE DonorId = @id";
                command.Parameters.AddWithValue("@id", donorId);
                command.ExecuteNonQuery();
            }
        }

        private DateTime GetDonorRestingPeriodEndDate(Guid donorId, SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT DATEADD(MONTH, RestingPeriodInMonths, LastDonationDate) as RestingPeriodEndDate " +
                    "FROM [BloodRush].[dbo].[DonorsRestingPeriodInfo] " +
                    "WHERE IsRestingPeriodActive = 1 AND DonorId = @id";

                command.Parameters.AddWithValue("@id", donorId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetDateTime(0);
                    }
                }
            }

            throw new Exception("Error while getting resting period end date");
        }

        private List<Guid> GetDonorsWithActiveRestingPeriod(SqlConnection connection)
        {
            var donorsWithActiveRestingPeriod = new List<Guid>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT DonorId FROM DonorsRestingPeriodInfo WHERE IsRestingPeriodActive = 1";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        donorsWithActiveRestingPeriod.Add(reader.GetGuid(0));
                    }
                }
            }

            return donorsWithActiveRestingPeriod;
        }
    }
}
