using Document.DataAccess.Context;
using Document.DataAccess.Repositories;
using Document.Store.Services;
using MassTransit;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, builder) =>
    {
        var configuration = builder
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("settings.json", true, true)
            .AddJsonFile("local.settings.json", true, false)
            .AddEnvironmentVariables()
            .Build();

        builder.AddUserSecrets<Program>();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<ISnapshotContext, SnapshotContext>();
        services.AddScoped<ISnapshotRepository, SnapshotRepository>();
        services.AddScoped<IStoreService, StoreService>();

        services.AddSingleton<CosmosClient>(service =>
        {
            return new CosmosClient(context.Configuration.GetConnectionString("CosmosDB"));
        });

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
