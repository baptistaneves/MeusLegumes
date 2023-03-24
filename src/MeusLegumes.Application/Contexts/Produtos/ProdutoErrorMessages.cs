namespace MeusLegumes.Application.Contexts.Produtos;

public class ProdutoErrorMessages
{
    public const string ProdutoJaExiste = "Já existe um produto cadastrado com este nome";
    public const string ProdutoNaoPodeSerRemovido = "Este produto possui pacotes associados, não pode ser removido";
    public const string ProdutoNaoEncotrado = "Não existe nenhum produto com o Id informado";
}
