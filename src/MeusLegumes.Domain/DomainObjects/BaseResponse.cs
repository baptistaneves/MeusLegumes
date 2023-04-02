namespace MeusLegumes.Domain.DomainObjects;

public class BaseResponse
{
    public bool Success { get; }
    public IEnumerable<ErrorResponse> Errors { get; }

    protected BaseResponse(bool success = false, IEnumerable<ErrorResponse> errors = null)
    {
        Success = success;
        Errors = errors;
    }
}
