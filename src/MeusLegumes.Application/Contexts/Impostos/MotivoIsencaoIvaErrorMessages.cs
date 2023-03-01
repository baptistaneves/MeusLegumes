namespace MeusLegumes.Application.Contexts.Impostos;

internal class MotivoIsencaoIvaErrorMessages
{
    public const string MotivoJaExiste = "Já existe um motivo cadastrado com este código";
    public const string MotivoNaoEncontrado = "Não existe nenhum motivo com o Id informado";
    public const string MotivoNaoPodeSerRemovido = "Este motivo possui produtos cadastrados não pode ser removido";
}
