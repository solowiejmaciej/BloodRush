using System;
using Azure.Storage.Queues.Models;
using BloodRush.Contracts.Events;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BloodRush.Tasks;

public class FindPotentialDonorsTask
{
    private readonly ILogger<FindPotentialDonorsTask> _logger;

    public FindPotentialDonorsTask(ILogger<FindPotentialDonorsTask> logger)
    {
        _logger = logger;
    }

    [Function(nameof(FindPotentialDonorsTask))]
    //[RabbitMQOutput(QueueName = RabbitQueues.NotificationsQueue, ConnectionStringSetting = "RabbitMQConnection")]
    public void Run([RabbitMQTrigger(RabbitQueues.BloodNeedCreated, ConnectionStringSetting = "RabbitMQConnection")] RabbitMqMessage item,
        FunctionContext context)
    {

        _logger.LogInformation($"C# function processed: {item.message.CollectionFacilityId}");
        // TODO: Find potential donors based on location and create send notifications event 

        if (item.message.IsUrgent)
        {
            _logger.LogInformation("Urgent need for blood");
        }

    }
    
    public class RabbitMqMessage
    {
        public  BloodNeedCreatedEvent message { get; set; }
    }
    
    
}

