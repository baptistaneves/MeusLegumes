namespace MeusLegumes.Domain.Communication.Commands;

public class Command : IRequest<bool>
{
    public ValidationResult ValidationResult { get; set; }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}
