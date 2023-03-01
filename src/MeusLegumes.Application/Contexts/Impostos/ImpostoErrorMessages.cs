namespace MeusLegumes.Application.Contexts.Impostos;

internal class ImpostoErrorMessages
{
    public const string ImpostoJaExiste = "Já existe um imposto cadastrado com este Nome";
    public const string ImpostoNaoEncontrado = "Não existe nenhum imposto com o Id informado";
    public const string ImpostoNaoPodeSerRemovido = "Este imposto possui produtos cadastrados não pode ser removido";
}
