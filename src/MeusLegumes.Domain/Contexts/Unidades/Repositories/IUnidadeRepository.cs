
namespace MeusLegumes.Domain.Contexts.Unidades.Repositories
{
    public  interface IUnidadeRepository : IRepository<Unidade> 
    {
        Task<Unidade> VerificarSeUnidadePossuiProdutosPorId(Guid id);
    }
}
