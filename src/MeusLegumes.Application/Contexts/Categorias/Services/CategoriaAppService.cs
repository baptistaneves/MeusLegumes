namespace MeusLegumes.Application.Contexts.Categorias.Services;

public class CategoriaAppService : BaseService, ICategoriaAppService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;
    public CategoriaAppService(INotifier notifier,
                               ICategoriaRepository categoriaRepository,
                               IMapper mapper) : base(notifier)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    public async Task Adicionar(CriarCategoria categoria, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarCategoriaValidation(), categoria)) return;

        if(_categoriaRepository.BuscarAsync(c => c.Descricao == categoria.Descricao).Result.Any())
        {
            Notify(CategoriaErrorMessages.CategoriaJaExiste);
            return;
        }

        _categoriaRepository.Adicionar(new Categoria(categoria.Descricao));
        await _categoriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarCategoria categoria, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarCategoriaValidation(), categoria)) return;

        if(!_categoriaRepository.BuscarAsync(c => c.Id == categoria.Id).Result.Any())
        {
            Notify(CategoriaErrorMessages.CategoriaNaoEncontrada);
            return;
        }

        if (_categoriaRepository.BuscarAsync(c => c.Descricao == categoria.Descricao && c.Id != categoria.Id).Result.Any())
        {
            Notify(CategoriaErrorMessages.CategoriaJaExiste);
            return;
        }

        _categoriaRepository.Actualizar(_mapper.Map<Categoria>(categoria));
        await _categoriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        if(_categoriaRepository.VerificarSeCategoriaPossuiProdutosPorId(id).Result.Produtos.Any())
        {
            Notify(CategoriaErrorMessages.CategoriaNaoPodeSerRemovida);
            return;
        }

        _categoriaRepository.Remover(id);
        await _categoriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Categoria> ObterPorIdAsync(Guid id)
    {
        return await _categoriaRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Categoria>> ObterTodosAsync()
    {
        return await _categoriaRepository.ObterTodosAsync();
    }

    public void Dispose()
    {
        _categoriaRepository.Dispose();
    }
}

