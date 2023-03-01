namespace MeusLegumes.Domain.Contexts.Pacotes.Repositories;

public interface IPacoteRepository : IRepository<Pacote>
{
    Task<IEnumerable<PacoteProduto>> ObterPacoteProdutosPorPacoteId(Guid pacoteId);
}
