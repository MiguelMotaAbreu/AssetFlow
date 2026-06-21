using System.ComponentModel;
using AssetFlow.Domain;
using AssetFlow.Infrastructure;

IAssetRepository repositorioAtivos = new AssetRepository();

void Menu()
{
    Console.Clear();
    Console.WriteLine("--------------------");
    Console.WriteLine("ASSET FLOW - CONSOLE");
    Console.WriteLine("--------------------");

    Console.WriteLine("1 - Cadastrar Ativo");
    Console.WriteLine("2 - Listar Ativos");
    Console.WriteLine("3 - Colocar Ativo em Manutenção");
    Console.WriteLine("4 - Alocar Ativo para um colaborador");
    Console.WriteLine("5 - Devolver Ativo para Estoque");
    Console.WriteLine("6 - Sair");
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
                break;
            }
            foreach(Asset ativoL in listaAtivos)
            {
                Console.WriteLine($"Nome do ativo: {ativoL.Name}");
                Console.WriteLine($"Número de patrimônio do ativo: {ativoL.BP}");
                Console.WriteLine($"Status do ativo: {ativoL.Status}");
                Console.WriteLine($"Localização do ativo: {ativoL.Location}");
                Console.WriteLine($"Ativo alocado para: {ativoL.AlocadoPara ?? "Ninguém (Disponível no Estoque)"}");
                Console.WriteLine("----------------------------------------------");
            }
            Console.WriteLine("Lista impressa na tela com sucesso.");
            Console.ReadKey();
            Console.WriteLine("Encerrando função e retornando ao menu.");
            Thread.Sleep(5000);
            break;
        case 3:
            Console.Clear();
            Console.Write("Insira o BP do ativo que deverá ser enviado para manutenção: ");
            string bpManutencao = Console.ReadLine();
            var ativoEspecifico = repositorioAtivos.ObterPorBP(bpManutencao);
            if (ativoEspecifico == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ativo com BP informado não encontrado");
                Console.ResetColor();
                Console.ReadKey();
                break;
            }
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
            Console.Clear();
            Console.Write("Digite o BP do Ativo para ser alocado: ");
            string bpAlocar = Console.ReadLine();
            var ativoAlocar = repositorioAtivos.ObterPorBP(bpAlocar);
            if ( ativoAlocar == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ativo com BP informado não encontrado");
                Console.ResetColor();
                Console.ReadKey();
                break;
            }
            Console.Write("Digite o nome do colaborador que irá receber o Ativo: ");
            string nomeColaborador = Console.ReadLine();
            Console.Write("Digite a nova localização do Ativo: ");
            string novaLocalizacao = Console.ReadLine();
            try
            {
                ativoAlocar.Alocar(nomeColaborador, novaLocalizacao);
                Console.WriteLine($"Ativo alocado com sucesso para {nomeColaborador} em {novaLocalizacao}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro de operação: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
            }
            Console.WriteLine("Voltando ao menu...");
            Thread.Sleep(5000);
            break;
        case 5:
            Console.Clear();
            Console.Write("Digite o BP do Ativo a ser devolvido para o Estoque: ");
            string bpDevolvido = Console.ReadLine();
            var ativoDevolver = repositorioAtivos.ObterPorBP(bpDevolvido);
            if (ativoDevolver == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ativo com BP informado não encontrado");
                Console.ResetColor();
                Console.ReadKey();
                break;
            }
            try
            {
                ativoDevolver.Devolver();
                Console.WriteLine("Ativo devolvido ao Estoque com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro de operação: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
            }
            Console.WriteLine("Voltando ao menu...");
            Thread.Sleep(5000);
            break;
        case 6:
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
