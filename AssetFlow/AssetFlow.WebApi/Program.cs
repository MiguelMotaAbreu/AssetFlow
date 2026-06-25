using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IAssetRepository, AssetRepository>();

var app = builder.Build();

app.MapGet("/api/ativos", (IAssetRepository assetRepository) =>
{
   return assetRepository.ObterTodos(); 
});

app.MapPost("/api/ativos", (IAssetRepository assetRepository, AssetCreateRequest assetRequest) =>
{
   try
   {
      var assetRegistered = new Asset(assetRequest.Name, assetRequest.BP);
      assetRepository.Adicionar(assetRegistered);
      return Results.Created($"/api/ativos/{assetRegistered.BP}", assetRegistered);
   }
   catch (ArgumentException ex)
   {
      return Results.BadRequest(new {ex.Message}); 
   }
});

app.Run();
public record AssetCreateRequest(string Name, string BP);
