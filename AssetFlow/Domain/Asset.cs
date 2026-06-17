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
        // Definir o valor inicial como Enum.Disponivel para padronizar assim que o objeto for instanciado
        Status = StatusAtivo.Disponivel;
        Name = name;
        Location = "Estoque";
        BP = bp;
    }
    public Guid Id { get; private set; }
    public string BP { get; private set; }
    public string Location { get; private set; }
    // Alterar o tipo da Propriedade Status para o Enum que iremos criar para padronizar as entradas nesta propriedade
    public StatusAtivo Status { get; private set; }
    
    public string Name { get; private set; }
    
    public void ColocarEmManutencao()
    {
        // Trocar as comparações para Enum.Disponivel e Enum.Quebrado
        if (Status == StatusAtivo.Disponivel || Status == StatusAtivo.Quebrado)
        {
           Status = StatusAtivo.EmManutencao; 
        } else
        {
            throw new InvalidOperationException("O ativo não pode ser colocado em manutenção com este status.");
        }
    }
}