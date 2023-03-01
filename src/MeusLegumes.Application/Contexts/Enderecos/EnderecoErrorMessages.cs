namespace MeusLegumes.Application.Contexts.Enderecos;

internal class EnderecosErrorMessages
{
    public const string ProvinciaJaExiste = "Já existe uma província cadastrada com este Nome";
    public const string ProvinciaNaoEncontrada = "Não existe nenhuma província com o Id informado";
    public const string ProvinciaNaoPodeSerRemovida = "Esta província possui munícipios cadastrados não pode ser removida";

    public const string MunicipioJaExiste = "Já existe um munícipio cadastrado com este Nome";
    public const string MunicipioNaoEncontrada = "Não existe nenhum munícipio com o Id informado";
    public const string MunicipioNaoPodeSerRemovido = "Este munícipio possui clientes cadastrados não pode ser removido";
}
