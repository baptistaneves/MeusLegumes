namespace MeusLegumes.Application.AutoMapper;

public class RequestsToEntitiesProfile : Profile
{
	public RequestsToEntitiesProfile()
	{
		//Categoria
		CreateMap<ActualizarCategoria, Categoria>();

        //Unidade
        CreateMap<ActualizarUnidade, Unidade>();

        //Endereços
        CreateMap<ActualizarProvincia, Provincia>();

        CreateMap<ActualizarMunicipio, Municipio>();

        //Impostos
        CreateMap<ActualizarImposto, Imposto>();

        CreateMap<ActualizarMotivoIsencaoIva, MotivoIsencaoIva>();

        //Clinte
        CreateMap<CriarCliente, Cliente>();
        CreateMap<ActualizarCliente, Cliente>();
    }
}
