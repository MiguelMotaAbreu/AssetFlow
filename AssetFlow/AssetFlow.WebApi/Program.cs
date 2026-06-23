var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Injeção de Dependências com Ciclo de Vida
// Singleton - Inicia a classe que executa, deixando-a na memória, AssetRepository, seguindo o Contrato, a Interface IAssetRepository, uma única vez até o momento de encerramento da API
// O uso de < > serve quando temos que passar Tipos como parâmetros de uma função
builder.Services.AddSingleton<IAssetRepository, AssetRepository>();

var app = builder.Build();

app.MapGet("/api/ativos", (IAssetRepository assetRepository) =>
{
   return assetRepository.ObterTodos(); 
});

app.Run();