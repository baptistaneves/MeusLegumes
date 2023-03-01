namespace MeusLegumes.Domain.Contexts.Impostos.Repositories
{
    public interface IImpostoRepository : IRepository<Imposto> 
    {
        Task<Imposto> VerificarSeImpostoPossuiProdutosPorId(Guid id);
    }
}
