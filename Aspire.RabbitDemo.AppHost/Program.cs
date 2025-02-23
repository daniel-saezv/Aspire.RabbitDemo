using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", "guest");
var password = builder.AddParameter("password", "guest");

var rabbitmq = builder.AddRabbitMQ("RabbitMQ", username, password, 5672)
    .WithManagementPlugin();

var apiService = builder.AddProject<Aspire_RabbitDemo_ApiService>("PublisherAPI")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder.AddProject<Aspire_RabbitDemo_Consumer_Web>("ConsumerWebFrontEnd")
    .WithReference(rabbitmq)
    .WaitFor(apiService);

builder.Build().Run();
