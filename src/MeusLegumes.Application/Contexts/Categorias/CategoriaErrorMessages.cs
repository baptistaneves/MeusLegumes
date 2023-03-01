namespace MeusLegumes.Application.Contexts.Categorias;

internal class CategoriaErrorMessages
{
    public const string CategoriaJaExiste = "Já existe uma categoria cadastrada com esta Descrição/Nome";
    public const string CategoriaNaoEncontrada = "Não existe nenhuma categoria com o Id informado";
    public const string CategoriaNaoPodeSerRemovida = "Esta categoria possui produtos cadastrados não pode ser removida";
}
