namespace MeusLegumes.Domain.Contexts.Pacotes.Entities;

public class Pacote : Entity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public bool EmPromocao { get; private set; }
    public decimal PrecoPromocional { get; private set; }
    public string ImagemUrl { get; private set; }
    public bool Activo { get; private set; }

    //EF Rel.

    private readonly List<PacoteProduto> _pacoteProdutos = new List<PacoteProduto>();
    public IEnumerable<PacoteProduto> PacotesProduto { get { return _pacoteProdutos; } }

    public Pacote(string nome, string descricao, decimal precoUnitario, bool promocao, decimal precoPromocional, string imagemUrl, bool activo)
    {
        Nome = nome;
        Descricao = descricao;
        PrecoUnitario = precoUnitario;
        EmPromocao = promocao;
        PrecoPromocional = precoPromocional;
        ImagemUrl = imagemUrl;
        Activo = activo;
    }

    //EF Rel.
    public Pacote() { }

    public void Activar() => Activo = true;
    public void Desactivar() => Activo = false;

    public void ActivarPromocao() => EmPromocao = true;
    public void DesactivarPromocao() => EmPromocao = false;

    public void AlterarImagem(string imagemUrl) => ImagemUrl = imagemUrl;

    public void AdicionarPacoteProduto(PacoteProduto pacoteProduto)
    {
        _pacoteProdutos.Add(pacoteProduto);
    }

    public void RemoverPacoteProduto(PacoteProduto pacoteProduto)
    {
        _pacoteProdutos.Remove(pacoteProduto);
    }
}
