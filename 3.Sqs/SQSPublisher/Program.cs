﻿
using Amazon.SQS;
using Amazon.SQS.Model;
using SQSPublisher;
using System.Text.Json;

var sqlClient = new AmazonSQSClient();

var customer = new CustomerCreated
{
    Id = Guid.NewGuid(),
    Email = "zakaria.bijoy@live.com",
    FullName = "Md Zakaria Masud",
    DateOfBirth = new DateTime(1994, 12, 11),
    GitHubUsername = "zakariabijoy",
};

var queueUrlResponse = await sqlClient.GetQueueUrlAsync("Customers");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CustomerCreated)
            }
        }
    }
    
};

var response = await sqlClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();