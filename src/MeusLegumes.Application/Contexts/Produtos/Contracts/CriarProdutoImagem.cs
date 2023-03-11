using Microsoft.AspNetCore.Http;

namespace MeusLegumes.Application.Contexts.Produtos.Contracts;

public class CriarProdutoImagem
{
    public string UrlImagem { get; private set; }

    public IFormFile Imagem { get; set; }

    public void AdicionarImagemUrl(string imageUrl) => UrlImagem = imageUrl;
}
