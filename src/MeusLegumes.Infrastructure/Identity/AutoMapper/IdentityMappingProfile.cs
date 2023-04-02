namespace MeusLegumes.Infrastructure.Identity.AutoMapper;

public class IdentityMappingProfile : Profile
{
	public IdentityMappingProfile()
	{
		CreateMap<Usuario, AppUser>().ReverseMap();
	}
}
