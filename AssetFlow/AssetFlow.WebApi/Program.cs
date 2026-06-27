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

app.MapPut("/api/ativos/{bp}/manutencao", (string bp, IAssetRepository assetRepository) =>
{
   var specAsset = assetRepository.ObterPorBP(bp);

   if (specAsset == null)
   {
      return Results.NotFound();
   }

   try
   {
      specAsset.ColocarEmManutencao();
      return Results.Ok();
   }
   catch (InvalidOperationException ex)
   {
      return Results.BadRequest(new {ex.Message});
   }   
});

app.MapPut("/api/ativos/{bp}/alocar", (string bp, AssetAllocateRequest assetAllocate, IAssetRepository assetRepository) =>
{
   var specAsset = assetRepository.ObterPorBP(bp);

   if (specAsset == null)
   {
      return Results.NotFound();
   }

   try
   {
     specAsset.Alocar(assetAllocate.nomeColaborador, assetAllocate.novaLocalizacao);
     return Results.Ok(); 
   }
   catch (Exception ex) // Escolha de exception genérica para abranger as duas possíveis exceções (InvalidOperation e Argument) em um catch apenas
   {
      return Results.BadRequest(new {ex.Message});
   }
});

app.MapPut("/api/ativos/{bp}/devolver", (string bp, IAssetRepository assetRepository) =>
{
   var specAsset = assetRepository.ObterPorBP(bp);

   if (specAsset == null)
   {
      return Results.NotFound();
   }

   try
   {
      specAsset.Devolver();
      return Results.Ok();
   }
   catch (InvalidOperationException ex)
   {
      return Results.BadRequest(new {ex.Message});
   }
});

app.Run();
public record AssetCreateRequest(string Name, string BP);
public record AssetAllocateRequest(string nomeColaborador, string novaLocalizacao);
