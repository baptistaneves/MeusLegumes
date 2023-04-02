namespace MeusLegumes.Domain.DomainObjects;

public class ErrorResponse
{
    public string Mensagem { get; private set; }


    public ErrorResponse(string mensagem)
    {
        Mensagem = mensagem;
    }
}
