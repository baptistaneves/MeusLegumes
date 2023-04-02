namespace MeusLegumes.Domain.Contexts.Usuarios.Dtos;

public class CriarUsuarioResponse : BaseResponse
{
    public Guid Id { get; }
    public CriarUsuarioResponse(Guid id, bool success = false, IEnumerable<ErrorResponse> errors = null) : base(success, errors)
    {
        Id = id;
    }
}
