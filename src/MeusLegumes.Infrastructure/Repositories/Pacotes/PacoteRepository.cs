using MeusLegumes.Domain.Contexts.Pacotes.Repositories;

namespace MeusLegumes.Infrastructure.Repositories.Pacotes
{
    public class PacoteRepository : Repository<Pacote>, IPacoteRepository
    {
        public PacoteRepository(ApplicationContext context) : base(context)
        {
        }

        public void AdicionarPacoteItem(PacoteItem item)
        {
            _context.PacoteItems.Add(item);
        }

        public async Task<bool> PacoteItemJaExiste(Guid pacoteId, Guid produtoId)
        {
            return await _context.PacoteItems.AsNoTracking().AnyAsync(i => i.PacoteId == pacoteId && i.ProdutoId == produtoId);
        }
    }
}
