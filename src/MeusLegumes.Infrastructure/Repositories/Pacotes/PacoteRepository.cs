using MeusLegumes.Domain.Contexts.Pacotes.Repositories;

namespace MeusLegumes.Infrastructure.Repositories.Pacotes
{
    public class PacoteRepository : Repository<Pacote>, IPacoteRepository
    {
        public PacoteRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
