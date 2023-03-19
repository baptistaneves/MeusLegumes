namespace MeusLegumes.Domain.Contexts.Pacotes.Repositories;

public interface IPacoteRepository : IRepository<Pacote>
{
    void AdicionarPacoteItem(PacoteItem item);
    Task<bool> PacoteItemJaExiste(Guid pacoteId, Guid produtoId);

}
