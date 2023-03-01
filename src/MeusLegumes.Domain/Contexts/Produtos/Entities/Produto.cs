namespace MeusLegumes.Domain.Contexts.Produtos.Entities;

public class Produto : Entity
{
    public Guid CategoriaId { get; private set; }
    public Guid UnidadeId { get; private set; }
    public Guid ImpostoId { get; private set; }
    public Guid MotivoId { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public string UrlImagemPrincipal { get; private set; }
    public bool Promocao { get; private set; }
    public decimal PrecoPromocional { get; private set; }
    public bool Destaque { get; private set; }
    public bool NovoLancamento { get; private set; }
    public bool MaisVendido { get; private set; }
    public bool EmEstoque { get; private set; }
    public bool Activo { get; private set; }

    //EF Rel.
    public Categoria Categoria { get; private set; }
    public Unidade Unidade { get; private set; }
    public Imposto Imposto { get; private set; }
    public MotivoIsencaoIva MotivoIsencaoIva { get; private set; }

    private readonly List<ProdutoImagem> _produtoImagens = new List<ProdutoImagem>();
    private readonly List<ProdutoRelacionado> _produtoRelacionados = new List<ProdutoRelacionado>();
    public IEnumerable<ProdutoImagem> ProdutoImagem { get { return _produtoImagens; } }
    public IEnumerable<ProdutoRelacionado> ProdutoRelacionado { get { return _produtoRelacionados; } }

    public Produto(Guid categoriaId, Guid unidadeId, string nome, string descricao, decimal precoUnitario, string urlImagemPrincipal, bool activo)
    {
        CategoriaId = categoriaId;
        UnidadeId = unidadeId;
        Nome = nome;
        Descricao = descricao;
        PrecoUnitario = precoUnitario;
        UrlImagemPrincipal = urlImagemPrincipal;
        Activo = activo;
    }

    public void Activar() => Activo = true;
    public void Desactivar() => Activo = false;

    public void ActivarPromocao() => Promocao = true;
    public void DesactivarPromocao() => Promocao = false;

    public void ActivarDestaque() => Destaque = true;
    public void DesactivarDestaque() => Destaque = false;

    public void ActivarNovoLancamento() => NovoLancamento = true;
    public void DesactivarNovoLancamento() => NovoLancamento = false;

    public void ActivarMaisVendido() => MaisVendido = true;
    public void DesactivarMaisVendido() => MaisVendido = false;

    public void ActivarEstoque() => EmEstoque = true;
    public void DesactivarEstoque() => EmEstoque = false;

    public void AlterarImagemPrincipal(string urlImagemPrincipal) => UrlImagemPrincipal = urlImagemPrincipal;

    public void AdicionarProdutoImagem(ProdutoImagem produtoImagem)
    {
        _produtoImagens.Add(produtoImagem);
    }

    public void RemoverProdutoImagemPorUrlImagem(ProdutoImagem produtoImagem)
    {
        _produtoImagens.Remove(produtoImagem);
    }

    public void AdicionarProdutoRelacionado(ProdutoRelacionado produtoRelacionado)
    {
        _produtoRelacionados.Add(produtoRelacionado);
    }
    void RemoverProdutoRelacionadoPorProdutoRelacionadoId(ProdutoRelacionado produtoRelacionado)
    {
        _produtoRelacionados.Remove(produtoRelacionado);
    }
}
