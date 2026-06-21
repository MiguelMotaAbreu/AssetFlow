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
    public string? AlocadoPara { get; private set; }
    public void Alocar(string nomeColaborador, string novaLocalizacao)
    {
        if (string.IsNullOrEmpty(nomeColaborador) || string.IsNullOrEmpty(novaLocalizacao))
        {
            throw new ArgumentException("É necessário informar quem irá receber o ativo e a nova localização dele.");
        }
        if (!(Status == StatusAtivo.Disponivel))
        {
            throw new InvalidOperationException("O ativo precisa estar disponível para ser alocado.");
        }
        Status = StatusAtivo.Alocado;
        Location = novaLocalizacao;
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

    // Criar Método Devolver()
    public void Devolver()
    {
        //  O Método Devolver() será responsável por tirar um ativo do status de Manutenção ou de retirar o ativo de algum colaborador, portanto, não faz sentido permitir que um ativo seja devolvido se o Status dele for Disponivel
        if (Status == StatusAtivo.Disponivel)
        {
            throw new InvalidOperationException("Não é possível devolver um Ativo que já está em Estoque");
        }
        //  O Método Devolver() irá alterar:
        //      Status para Disponivel
        Status = StatusAtivo.Disponivel;
        //      Location para "Estoque"
        Location = "Estoque";
        //      AlocadoPara para null
        AlocadoPara = null;       
    }
}