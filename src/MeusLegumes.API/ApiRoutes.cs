namespace MeusLegumes.API;

public class ApiRoutes
{
    public const string BaseRoute = "api/v{version:ApiVersion}/[controller]";

    public static class Categoria
    {
        public const string ObterCategorias = "obter-categorias";
        public const string ObterCategoriaPorId = "obter-categoria-por-id/{id}";
        public const string NovaCategoria = "nova-categoria";
        public const string ActualizarCategoria = "actualizar-categoria";
        public const string RemoverCategoria = "remover-categoria/{id}";
    }

    public static class Unidade
    {
        public const string ObterUnidades = "obter-unidades";
        public const string ObterUnidadePorId = "obter-unidade-por-id/{id}";
        public const string NovaUnidade = "nova-unidade";
        public const string ActualizarUnidade = "actualizar-unidade";
        public const string RemoverUnidade = "remover-unidade/{id}";
    }

    public static class Provincia
    {
        public const string ObterProvincias = "obter-provincias";
        public const string ObterProvinciaPorId = "obter-provincia-por-id/{id}";
        public const string NovaProvincia = "nova-provincia";
        public const string ActualizarProvincia = "actualizar-provincia";
        public const string RemoverProvincia = "remover-provincia/{id}";

        public const string ObterMunicipios = "obter-municipios";
        public const string ObterMunicipioPorId = "obter-municipio-por-id/{id}";
        public const string NovoMunicipio = "nova-municipio";
        public const string ActualizarMunicipio = "actualizar-municipio";
        public const string RemoverMunicipio = "remover-municipio/{id}";
    }

    public static class Imposto
    {
        public const string ObterImpostos = "obter-impostos";
        public const string ObterImpostoPorId = "obter-imposto-por-id/{id}";
        public const string NovoImposto = "novo-imposto";
        public const string ActualizarImposto = "actualizar-imposto";
        public const string RemoverImposto = "remover-imposto/{id}";
    }

    public static class MotivoIsencaoIva
    {
        public const string ObterMotivos = "obter-motivos";
        public const string ObterMotivoPorId = "obter-motivo-por-id/{id}";
        public const string NovoMotivo = "novo-motivo";
        public const string ActualizarMotivo = "actualizar-motivo";
        public const string RemoverMotivo = "remover-motivo/{id}";
    }

    public static class Cliente
    {
        public const string ObterClientes = "obter-clientes";
        public const string ObterClientePorId = "obter-cliente-por-id/{id}";
        public const string NovoCliente = "novo-cliente";
        public const string ActualizarCliente = "actualizar-cliente";
        public const string RemoverCliente = "remover-cliente/{id}";
    }

    public static class Produto
    {
        public const string ObterProdutos = "obter-produtos";
        public const string ObterProdutoPorId = "obter-produto-por-id/{id}";
        public const string NovoProduto = "novo-produto";
        public const string ActualizarProduto = "actualizar-produto";
        public const string RemoverProduto = "remover-produto/{id}";
        public const string UploadImagem = "upload-de-imagem";
        public const string UploadMultiplasImagens = "upload-de-multiplas-imagens";
    }

    public static class Pacote
    {
        public const string ObterPacotes = "obter-pacotes";
        public const string ObterPacotePorId = "obter-pacote-por-id/{id}";
        public const string NovoPacote = "novo-pacote";
        public const string ActualizarPacote = "actualizar-pacote";
        public const string RemoverPacote = "remover-pacote/{id}";
        public const string UploadImagem = "upload-de-imagem";
    }

    public static class Usuario
    {
        public const string CriarUsuario = "criar-usuario";
        public const string Login = "login";
    }

    public class Pedido
    {
        public const string MeuCarrinho = "meu-carrinho";
        public const string RemoverItemDoCarrinho = "remover-item-no-carrinho";
        public const string ActualizarItemNoCarrinho = "actualizar-item-no-carrinho";
        public const string AdicionarItemNoCarrinho = "adicionar-item-no-carrinho";
        public const string ResumoDaCompra = "resumo-da-compra";
        public const string IniciarPedido = "iniciar-pedido";
        public const string FinalizarPedido = "finalizar-pedido";
    }
}
