using System.ComponentModel;

var repositorioAtivos = new AssetRepository();

void Menu()
{
    Console.Clear();
    Console.WriteLine("--------------------");
    Console.WriteLine("ASSET FLOW - CONSOLE");
    Console.WriteLine("--------------------");

    Console.WriteLine("1 - Cadastrar Ativo");
    Console.WriteLine("2 - Listar Ativos");
    Console.WriteLine("3 - Colocar Ativo em Manutenção");
    Console.WriteLine("4 - Sair");
    //Adicionar Opção de Envio Para Manutenção utilizando o metódo ColocarEmManutencao()
    Console.WriteLine("-------------------------------");

    int.TryParse(Console.ReadLine(), out int respNum);

    switch (respNum)
    {
        case 1:
            Console.Clear();
            Console.Write("Digite o nome do ativo a ser registrado: ");
            string name = Console.ReadLine();
            Console.Write("Digite o número de patrimônio do ativo a ser registrado: ");
            string bp = Console.ReadLine();
            try
            {
                var ativo = new Asset(name, bp);
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
            Console.Clear();
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
            Console.Clear();
            // Solicitar entrada do BP para o usuário
            Console.Write("Insira o BP do ativo que deverá ser enviado para manutenção: ");
            string bpManutencao = Console.ReadLine();
            // Chamar o método ObterPorBP() para encontrar o ativo em específico na memória
            var ativoEspecifico = repositorioAtivos.ObterPorBP(bpManutencao);
            // Se o ativo NÃO for encontrado (retornar nulo), exibir uma mensagem em vermelho ou amarelo dizendo: "Ativo com BP informado não encontrado."
            if (ativoEspecifico == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ativo com BP informado não encontrado");
                Console.ResetColor();
                Console.ReadKey();
                break;
            }
            // Se o ativo FOR encontrado, lembre-se de utilizar try-catch por conta da exceção que faz a mensuração do Status do ativo. Funcionando de forma semelhante ao Case 1.
            try
            {
                ativoEspecifico.ColocarEmManutencao();
                Console.WriteLine("Ativo enviado para manutenção com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro de operação: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
            }   
            Console.WriteLine("Encerrando função e retornando ao menu.");
            Thread.Sleep(5000);
            break;
        case 4:
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
