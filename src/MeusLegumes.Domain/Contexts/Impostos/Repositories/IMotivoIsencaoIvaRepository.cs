namespace MeusLegumes.Domain.Contexts.Impostos.Repositories
{
    public interface IMotivoIsencaoIvaRepository : IRepository<MotivoIsencaoIva> 
    {
        Task<MotivoIsencaoIva> VerificarSeMotivoPossuiProdutosPorId(Guid id);
    }
}
