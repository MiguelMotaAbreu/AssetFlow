public class Asset
{
    public Asset(string name, string bp)
    {
        
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("O nome do ativo não pode ser vazio.");
        }

        if (string.IsNullOrEmpty(bp))
        {
            throw new ArgumentException("O BP do ativo não pode ser vazio.");
        }
        Id = Guid.NewGuid();
        Status = StatusAtivo.Disponivel;
        Name = name;
        Location = "Estoque";
        BP = bp;
    }
    public Guid Id { get; private set; }
    public string BP { get; private set; }
    public string Location { get; private set; }
    public StatusAtivo Status { get; private set; }    
    public string Name { get; private set; }
    // Criar Propriedade AlocadoPara que pode ser nula já que o ativo entra em estoque sem um dono definido
    public string? AlocadoPara { get; private set; }
    // Criar Método Alocar() com parâmetros de nome do colaborador que passará a ser o dono honorário do ativo e a localização
    public void Alocar(string nomeColaborador, string novaLocalizacao)
    {
        // Se qualquer um dos parâmetros forem entrados com valores nulos ou vazios, o Método deve disparar uma ArgumentException
        if (string.IsNullOrEmpty(nomeColaborador) || string.IsNullOrEmpty(novaLocalizacao))
        {
            throw new ArgumentException("É necessário informar quem irá receber o ativo e a nova localização dele.");
        }
        // Um ativo só pode ser alocado se estiver com o status atual como StatusAtivo.Disponivel, se esse não for o caso, o Método deve disparar uma InvalidOperationException
        if (!(Status == StatusAtivo.Disponivel))
        {
            throw new InvalidOperationException("O ativo precisa estar disponível para ser alocado.");
        }
        // Caso passe pelas duas validações:
        // O Status deve mudar para StatusAtivo.Alocado
        Status = StatusAtivo.Alocado;
        // Location passa ter o valor do parâmetro localização de Alocar()
        Location = novaLocalizacao;
        // AlocadoPara deve passar a ter o valor do nome do colaborador que está como dono
        AlocadoPara = nomeColaborador;
    }
    public void ColocarEmManutencao()
    {
        if (Status == StatusAtivo.Disponivel || Status == StatusAtivo.Quebrado)
        {
           Status = StatusAtivo.EmManutencao; 
        } else
        {
            throw new InvalidOperationException("O ativo não pode ser colocado em manutenção com este status.");
        }
    }
}