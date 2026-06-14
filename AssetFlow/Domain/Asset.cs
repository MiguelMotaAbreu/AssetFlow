public class Asset
{
    public Asset(string name, string status, string bp)
    {
        Id = Guid.NewGuid();
        Status = status;
        Name = name;
        Location = "Estoque";
        BP = bp;

        // Validações básicas para garantir que os campos essenciais não sejam nulos ou vazios

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("O nome do ativo não pode ser vazio.");
        }

        if (string.IsNullOrEmpty(status))
        {
            throw new ArgumentException("O status do ativo não pode ser vazio.");
        }

        if (string.IsNullOrEmpty(bp))
        {
            throw new ArgumentException("O BP do ativo não pode ser vazio.");
        }
    }
    public Guid Id { get; private set; }
    // O Id servirá para identificar os ativos somente dentro do sistema, para busca e consulta dos dados, será utilizado o BP (Bem Patrimonial)
    public string BP { get; private set; }
    // O número do Bem Patrimonial, é o número de identificação do ativo, usado para identificação do ativo no mundo físico 
    public string Location { get; private set; }
    // A localização do ativo, em que sala, por exemplo, ele se encontra
    public string Status { get; private set; }
    // O status do ativo, se ele está disponível, em manutenção, etc.
    public string Name { get; private set; }
    // O nome do ativo, para facilitar a diferenciação entre ativos que estejam na mesma sala

    //Método para colocar um ativo em manutenção
    public void ColocarEmManutencao(string name, string status, string bp)
    {
        //Verificação que impede o direcionamento de um ativo para manutenção se este estiver em uso.
        if (status != "Alocado")
        {
            Status = "EmManutencao";
        } else
        {
            throw new InvalidOperationException("O computador não pode ser colocado em manutenção pois está em uso. Primeiro, retire-o.");
        }
    }
}