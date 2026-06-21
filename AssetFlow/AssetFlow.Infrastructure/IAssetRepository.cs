public interface IAssetRepository
{
    public void Adicionar(Asset asset);
    public Asset? ObterPorBP(string BP);
    public IEnumerable<Asset> ObterTodos();
}