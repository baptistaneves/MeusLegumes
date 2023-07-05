namespace MeusLegumes.Application.Contexts.Categorias;

public class CategoriaErrorMessages
{
    public const string CategoriaJaExiste = "Já existe uma categoria cadastrada com esta Descrição/Nome";
    public const string CategoriaNaoEncontrada = "Não existe nenhuma categoria com o Id informado";
    public const string CategoriaNaoPodeSerRemovida = "Esta categoria possui produtos cadastrados não pode ser removida";
    public const string CategoriaNaoPodeSerVazia = "O nome da categoria deve ser informado";
    public const string CategoriaComMinimoDeCaracteres = "O nome da categoria deve ter no minimo 4 caracteres";
}
