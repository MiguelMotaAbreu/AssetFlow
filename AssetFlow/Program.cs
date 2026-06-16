// Instanciar AssetRepository como um Objeto
using System.ComponentModel;

var repositorioAtivos = new AssetRepository();

// Menu interativo da aplicação com While:
// 1 - Cadastrar Ativo
// 2 - Listar Ativos
// 3 - Sair

void Menu() // Retirada de chamadas desnecessárias do método dentro dele mesmo para evitar stackoverflow
{
    Console.Clear();
    Console.WriteLine("--------------------");
    Console.WriteLine("ASSET FLOW - CONSOLE");
    Console.WriteLine("--------------------");

    Console.WriteLine("1 - Cadastrar Ativo");
    Console.WriteLine("2 - Listar Ativos");
    Console.WriteLine("3 - Sair");
    Console.WriteLine("--------------------");

    int.TryParse(Console.ReadLine(), out int respNum);

    switch (respNum)
    {
        case 1:
            Console.Clear();
            Console.Write("Digite o nome do ativo a ser registrado: ");
            string name = Console.ReadLine();
            Console.WriteLine("");
            Console.Write("Digite o número de patrimônio do ativo a ser registrado");
            string bp = Console.ReadLine();
            try
            {
                var ativo = new Asset(name, bp); // Correção para impedir um disparar da exceção e encerramento do programa em caso de preenchimento vazio por parte do usuário.
                repositorioAtivos.Adicionar(ativo);
                Console.WriteLine("Ativo registrado no repositório com sucesso!");
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro de validação: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
            }

            Console.WriteLine("Encerrando função e retornando ao menu.");
            Thread.Sleep(5000);
            break;
        case 2:
            var listaAtivos = repositorioAtivos.ObterTodos();
            if (listaAtivos.Count() == 0)
            {
                Console.WriteLine("Lista de ativos vazia... Encerrando função e retornando ao menu.");
                Thread.Sleep(5000);
            }
            foreach(Asset ativoL in listaAtivos)
            {
                Console.WriteLine($"Nome do ativo: {ativoL.Name}");
                Console.WriteLine($"Número de patrimônio do ativo: {ativoL.BP}");
                Console.WriteLine($"Status do ativo: {ativoL.Status}");
                Console.WriteLine($"Localização do ativo: {ativoL.Location}");
                Console.WriteLine("----------------------------------------------");
            }
            Console.WriteLine("Lista impressa na tela com sucesso.");
            Console.ReadKey();
            Console.WriteLine("Encerrando função e retornando ao menu.");
            Thread.Sleep(5000);
            break;
        case 3:
            Console.WriteLine("Encerrando aplicação...");
            Thread.Sleep(5000);
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Entrada incorreta não detectada, reiniciando menu.");
            Thread.Sleep(5000);
            break;
    }
}
bool ok = true;
while (ok)
{
    Menu();
}

// Opção 1 da Aplicação, Cadastrar Ativo:
//  Solicitar entrada de Nome e BP para o usuário
//  Instanciar um Objeto da Classe Asset passando os dados de entrada utilizados
//  Usar o método Adicionar
//  UTILIZAR TRATAMENTO DE ERROS COM TRY-CATCH

// Opção 2 da Aplicação, Listar Ativos
//  Usar o Método ObterTodos()
//  Utilizar um foreach para percorrer toda a lista e imprimir na tela os ativos contendo: Nome; BP; Status e Localização.
//  Tratar o fluxo com mensagem amigável em caso de ativos não registrados
