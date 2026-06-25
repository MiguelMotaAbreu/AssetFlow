using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Injeção de Dependências com Ciclo de Vida
// Singleton - Inicia a classe que executa, deixando-a na memória, AssetRepository, seguindo o Contrato, a Interface IAssetRepository, uma única vez até o momento de encerramento da API
// O uso de < > serve quando temos que passar Tipos como parâmetros de uma função
builder.Services.AddSingleton<IAssetRepository, AssetRepository>();

var app = builder.Build();

// Rota para listar todos os ativos registrados
app.MapGet("/api/ativos", (IAssetRepository assetRepository) =>
{
   return assetRepository.ObterTodos(); 
});

// Rota para registrar novos ativos
// ATT - Como as rotas já se diferenciam pela sua própria função, é redundante e desnecessário colocar que uma rota de POST serve para registrar
app.MapPost("/api/ativos/registrar", (IAssetRepository assetRepository, AssetCreateRequest assetRequest) =>
{
   // try-catch aninhado para conseguir capturar as duas exceções possíveis ao criar um ativo, objeto do tipo Asset, e ao inserí-lo na lista de ativos
   try
   {
      var assetRegistered = new Asset(assetRequest.Name, assetRequest.BP);
      try
      {
         assetRepository.Adicionar(assetRegistered);
         Results.Created(); // ATT - É necessário especificar o código de retorno, 400, 200 etc. Além disso, falta especificar o return propriamente dito.
      }
      catch (ArgumentException ex)
      {
         Results.BadRequest(ex.Message);
      }
   } // ATT - Como os dois catch estão capturando a mesma exceção, não tem necessidade, neste caso, de fazer um try-catch aninhado
   catch (ArgumentException ex) // Captura os erros de validação do método construtor de Asset
   {
      Results.BadRequest(ex.Message);
   }
});

app.Run();

// Embora, a priori, pareça esquisito colocar a declaração do record no final do arquivo, isso acontece porque o record é uma estrutura de nível superior. Como já sabemos, o compilador lê o código em fases, destrinchando dos níveis mais altos para os mais baixos. Para evitar que dividamos o nosso código em estruturas e focando em manter o código limpo, podemos colocar a declaração dele ao final do código para contornar o erro de "incompatibilidade de níveis".
public record AssetCreateRequest(string Name, string BP);
