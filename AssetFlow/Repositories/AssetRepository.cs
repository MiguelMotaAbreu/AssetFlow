public class AssetRepository
{
    private readonly List<Asset> _assets = new();
    public void Adicionar(Asset asset)
    {
        if(ObterPorBP(asset.BP) != null)
        {
            throw new ArgumentException("O BP inserido já está cadastrado.");
        }
        _assets.Add(asset);
    }
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
    public IEnumerable<Asset> ObterTodos()
    {
        return _assets.AsReadOnly();
    }
}