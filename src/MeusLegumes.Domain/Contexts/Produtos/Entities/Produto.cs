namespace MeusLegumes.Domain.Contexts.Produtos.Entities;

public class Produto : Entity
{
    public Guid? CategoriaId { get; private set; }
    public Guid? UnidadeId { get; private set; }
    public Guid? ImpostoId { get; private set; }
    public Guid? MotivoId { get; private set; }
    public string Nome { get; private set; }
    public TipoProduto Tipo { get; private set; }
    public string Descricao { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public string UrlImagemPrincipal { get; private set; }
    public bool EmPromocao { get; private set; }
    public decimal? PrecoPromocional { get; private set; }
    public bool Destaque { get; private set; }
    public bool NovoLancamento { get; private set; }
    public bool MaisVendido { get; private set; }
    public bool MaisProcurado { get; private set; }
    public bool EmEstoque { get; private set; }
    public bool Activo { get; private set; }
    public string Observacao { get; private set; }

    //EF Rel.
    public Categoria Categoria { get; private set; }
    public Unidade Unidade { get; private set; }
    public Imposto Imposto { get; private set; }
    public MotivoIsencaoIva MotivoIsencaoIva { get; private set; }

    private readonly List<ProdutoImagem> _produtoImagens = new List<ProdutoImagem>();
    private readonly List<ProdutoRelacionado> _produtoRelacionados = new List<ProdutoRelacionado>();

    public IEnumerable<ProdutoImagem> ProdutosImagem { get { return _produtoImagens; } }
    public IEnumerable<ProdutoRelacionado> ProdutosRelacionado { get { return _produtoRelacionados; } }

    public Produto(Guid categoriaId, Guid unidadeId, Guid impostoId, Guid motivoId, string nome,
        string descricao, decimal precoUnitario, string urlImagemPrincipal, bool emPromocao, 
        decimal precoPromocional, bool destaque, bool novoLancamento, bool maisVendido, 
        bool maisProcurado, bool emEstoque, bool activo, string observacao)
    {
        CategoriaId = categoriaId;
        UnidadeId = unidadeId;
        ImpostoId = impostoId;
        MotivoId = motivoId;
        Nome = nome;
        Tipo = TipoProduto.Produto;
        Descricao = descricao;
        PrecoUnitario = precoUnitario;
        UrlImagemPrincipal = urlImagemPrincipal;
        EmPromocao = emPromocao;
        PrecoPromocional = precoPromocional;
        Destaque = destaque;
        NovoLancamento = novoLancamento;
        MaisVendido = maisVendido;
        MaisProcurado = maisProcurado;
        EmEstoque = emEstoque;
        Activo = activo;
        Observacao = observacao;
    }

    //For EF
    public Produto() { }
   
    public void ActualizarProduto(Guid categoriaId, Guid unidadeId, Guid impostoId, Guid motivoId, string nome,
        string descricao, decimal precoUnitario, string urlImagemPrincipal, bool emPromocao,
        decimal precoPromocional, bool destaque, bool novoLancamento, bool maisVendido,
        bool maisProcurado, bool emEstoque, bool activo, string observacao)
    {
        CategoriaId = categoriaId;
        UnidadeId = unidadeId;
        ImpostoId = impostoId;
        MotivoId = motivoId;
        Nome = nome;
        Descricao = descricao;
        PrecoUnitario = precoUnitario;
        UrlImagemPrincipal = urlImagemPrincipal;
        EmPromocao = emPromocao;
        PrecoPromocional = precoPromocional;
        Destaque = destaque;
        NovoLancamento = novoLancamento;
        MaisVendido = maisVendido;
        MaisProcurado = maisProcurado;
        EmEstoque = emEstoque;
        Activo = activo;
        Observacao = observacao;
    }

    public static Produto NovoPacote(string nome, string descricao, decimal precoUnitario, bool emPromocao, decimal precoPromocional,  string urlImagemPrincipal, bool activo, Guid unidadeId)
    {
        return new Produto
        {
             Nome = nome,
             UnidadeId = unidadeId,
             UrlImagemPrincipal = urlImagemPrincipal,
             Descricao = descricao,
             PrecoUnitario = precoUnitario,
             EmPromocao = emPromocao,
             PrecoPromocional = precoPromocional,
             Activo = activo,
             Tipo = TipoProduto.Pacote
        };
    }

    public void ActualizarPacote(string nome, string descricao, decimal precoUnitario, bool emPromocao, decimal precoPromocional, string urlImagemPrincipal, bool activo, Guid unidadeId)
    {
        Nome = nome;
        UrlImagemPrincipal = urlImagemPrincipal;
        UnidadeId = unidadeId;
        Descricao = descricao;
        PrecoUnitario = precoUnitario;
        EmPromocao = emPromocao;
        PrecoPromocional = precoPromocional;
        Activo = activo;
    }

    public void Activar() => Activo = true;
    public void Desactivar() => Activo = false;

    public void ActivarPromocao() => EmPromocao = true;
    public void DesactivarPromocao() => EmPromocao = false;

    public void ActivarDestaque() => Destaque = true;
    public void DesactivarDestaque() => Destaque = false;

    public void ActivarNovoLancamento() => NovoLancamento = true;
    public void DesactivarNovoLancamento() => NovoLancamento = false;

    public void ActivarMaisVendido() => MaisVendido = true;
    public void DesactivarMaisVendido() => MaisVendido = false;

    public void ActivarEstoque() => EmEstoque = true;
    public void DesactivarEstoque() => EmEstoque = false;

    public void AlterarImagemPrincipal(string urlImagemPrincipal) => UrlImagemPrincipal = urlImagemPrincipal;

    private bool ProdutoImagemExistente(ProdutoImagem produtoImagem)
    {
        return _produtoImagens.Any(pr => pr.UrlImagem == produtoImagem.UrlImagem);
    }

    public void AdicionarProdutoImagem(ProdutoImagem produtoImagem)
    {
        if (!ProdutoImagemExistente(produtoImagem)) 
        {
            produtoImagem.AssociarAoProduto(Id);

            _produtoImagens.Add(produtoImagem);
        }
    }

    public void RemoverProdutoImagem(ProdutoImagem produtoImagem)
    {
        _produtoImagens.Remove(produtoImagem);
    }

    private bool ProdutoRelacionadoExistente(ProdutoRelacionado produtoRelacionado)
    {
        return _produtoRelacionados.Any(pr => pr.ProdutoRelacionadoId == produtoRelacionado.ProdutoRelacionadoId);
    }

    public void AdicionarProdutoRelacionado(ProdutoRelacionado produtoRelacionado)
    {
        if (!ProdutoRelacionadoExistente(produtoRelacionado))
        {
            produtoRelacionado.AssociarAoProduto(Id);

            _produtoRelacionados.Add(produtoRelacionado);
        }

    }
    public void RemoverProdutoRelacionado(ProdutoRelacionado produtoRelacionado)
    {
        _produtoRelacionados.Remove(produtoRelacionado);
    }

}
