public class AssetRepository
{
    // Lista de assets que não pode ser acessada diretamente, só pode ser acessada e/ou modificada pelos outros métodos da classe AssetRepository
    private readonly List<Asset> _assets = new();
    // Método para adicionar um asset dentro da lista _assets, verifica se um asset com o mesmo BP já não foi inserido anteriormente, devolve uma exceção se este for o caso
    public void Adicionar(Asset asset)
    {
        if(ObterPorBP(asset.BP) != null)
        {
            throw new ArgumentException("O BP inserido já está cadastrado.");
        }
        _assets.Add(asset);
    }
    // Método para buscar na lista inteira um ativo que tenha o BP compatível com o inserido no parâmetro, retorna nulo caso não encontre
    public Asset? ObterPorBP(string BP)
    {
        foreach (Asset asset in _assets)
        {
            if (asset.BP == BP)
            {
                return asset;
            }
        }
        return null;
    }
    // Método que retorna a lista de maneira protegida para impedir o acesso e manipulação direta, fazendo uso também do AsReadOnly, que impede o uso de qualquer método de List que envolva registro nesta lista.
    public IEnumerable<Asset> ObterTodos()
    {
        return _assets.AsReadOnly();
    }
}