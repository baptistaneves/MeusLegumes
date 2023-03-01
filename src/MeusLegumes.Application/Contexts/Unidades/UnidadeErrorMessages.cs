namespace MeusLegumes.Application.Contexts.Unidades;

internal class UnidadeErrorMessages
{
    public const string UnidadeJaExiste = "Já existe uma unidade cadastrada com esta Descrição/Nome";
    public const string UnidadeNaoEncontrada = "Não existe nenhuma unidade com o Id informado";
    public const string UnidadeNaoPodeSerRemovida = "Esta unidade possui produtos cadastrados não pode ser removida";
}
