﻿using DocumentFormat.OpenXml.Office2010.Excel;

namespace MeusLegumes.Application.Contexts.Pacotes.Services;

public class PacoteAppService : BaseService, IPacoteAppService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    public PacoteAppService(INotifier notifier,
                            IProdutoRepository produtoRepository, 
                            IMapper mapper) : base(notifier)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }
    public async Task Adicionar(CriarPacote pacote, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarPacoteValidation(), pacote)) return;

        if (PacoteJaExiste(pacote.Nome)) return;

        var novoPacote = Produto.NovoPacote(pacote.Nome, pacote.Descricao, pacote.PrecoUnitario, pacote.EmPromocao, pacote.PrecoPromocional, pacote.UrlImagemPrincipal, pacote.Activo, pacote.UnidadeId);

        if (pacote.ProdutosRelacionados.Any())
        {
            foreach (var produtoRelacionado in pacote.ProdutosRelacionados)
            {
                novoPacote.AdicionarProdutoRelacionado(new ProdutoRelacionado(produtoRelacionado));
            }
        }

        if (pacote.ImagensOpcionaisUrls.Any())
        {
            foreach (var imagemUrl in pacote.ImagensOpcionaisUrls)
            {
                novoPacote.AdicionarProdutoImagem(new ProdutoImagem(imagemUrl));
            }
        }

        _produtoRepository.Adicionar(novoPacote);
        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarPacote pacoteAct, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarPacoteValidation(), pacoteAct)) return;

        var pacote = await _produtoRepository.ObterPacotePorId(pacoteAct.Id);

        if (PacoteJaExiste(pacoteAct.Nome, pacoteAct.Id)) return;

        pacote.ActualizarPacote(pacoteAct.Nome, pacoteAct.Descricao, pacoteAct.PrecoUnitario, pacoteAct.EmPromocao, pacoteAct.PrecoPromocional, pacoteAct.UrlImagemPrincipal, pacoteAct.Activo, pacoteAct.UnidadeId); ;

        if (pacoteAct.ProdutosRelacionados.Any())
        {
            foreach (var produtoRelacionadoId in pacoteAct.ProdutosRelacionados)
            {
                if (!await _produtoRepository.ExisteRelacaoComProduto(pacote.Id, produtoRelacionadoId))
                {
                    var produtoRelacionado = new ProdutoRelacionado(produtoRelacionadoId);
                    produtoRelacionado.AssociarAoProduto(pacote.Id);
                    _produtoRepository.AdicionarProdutoRelacionado(produtoRelacionado);
                }
            }
        }

        if (pacoteAct.ImagensOpcionaisUrls.Any())
        {
            foreach (var imagemUrl in pacoteAct.ImagensOpcionaisUrls)
            {
                var produtoImagem = new ProdutoImagem(imagemUrl);
                produtoImagem.AssociarAoProduto(pacote.Id);
                _produtoRepository.AdicionarProdutoImagem(produtoImagem);
            }
        }

        _produtoRepository.Actualizar(pacote);
        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        _produtoRepository.Remover(id);
        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Produto> ObterPorIdAsync(Guid id)
    {
        return await _produtoRepository.ObterPacotePorId(id);
    }

    public async Task<IEnumerable<PacoteDto>> ObterTodosAsync()
    {
        return await _produtoRepository.ObterTodosPacotes();
    }

    private bool PacoteJaExiste(string nome)
    {
        if (_produtoRepository.BuscarAsync(c => c.Nome == nome).Result.Any())
        {
            Notify(PacoteErrorMessages.PacoteJaExiste);
            return true;
        }

        return false;
    }

    private bool PacoteJaExiste(string nome, Guid id)
    {
        if (_produtoRepository.BuscarAsync(c => c.Nome == nome && c.Id != id).Result.Any())
        {
            Notify(PacoteErrorMessages.PacoteJaExiste);
            return true;
        }

        return false;
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
    }
}
