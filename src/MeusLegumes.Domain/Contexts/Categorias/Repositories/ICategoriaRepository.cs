namespace MeusLegumes.Domain.Contexts.Categorias.Repositories;

public interface ICategoriaRepository : IRepository<Categoria> 
{
    Task<Categoria> VerificarSeCategoriaPossuiProdutosPorId(Guid id);

}
