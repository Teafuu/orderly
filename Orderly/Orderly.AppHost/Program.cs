var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("OrderlyCache");

var apiService = builder.AddProject<Projects.Orderly_ApiService>("OrderlyApi");

builder.AddProject<Projects.Orderly_Web>("OrderlyWeb")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
