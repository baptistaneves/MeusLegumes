namespace MeusLegumes.Application.Contexts.Clientes;

internal class ClientesErrorMessages
{
    public const string ClienteJaExiste = "Já existe um cliente cadastrado com este endereço de email";
    public const string ClienteNaoEncontrado = "Não existe nenhum cliente com o Id informado";
    public const string ClienteNaoPodeSerRemovido = "Este cliente possui pedidos cadastrados não pode ser removido";
}
