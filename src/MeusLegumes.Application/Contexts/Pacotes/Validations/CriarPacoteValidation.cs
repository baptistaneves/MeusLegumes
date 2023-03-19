namespace MeusLegumes.Application.Contexts.Pacotes.Validations;

internal class CriarPacoteValidation : AbstractValidator<CriarPacote>
{
	public CriarPacoteValidation()
	{
		RuleFor(p => p.Nome)
			.NotEmpty().WithMessage("Informe o nome");

        RuleFor(p => p.PrecoUnitario)
            .NotEmpty().WithMessage("Informe o preço unitário do pacote")
            .GreaterThan(20).WithMessage("O valor do preço do pacote deve ser maior que vinte (20)");

        RuleFor(p => p.ImagemUrl)
            .NotEmpty().WithMessage("Selecione uma imagem para o pacote");

        When(p => p.EmPromocao == true, () =>
        {
            RuleFor(p => p.PrecoPromocional)
           .NotEmpty().WithMessage("Informe o preço promocional do pacote")
           .GreaterThan(20).WithMessage("O valor do preço promocional deve ser maior que vinte (20)");
        });

        RuleForEach(p => p.PacoteItems).SetValidator(new CriarPacoteProdutoValidation());
    }
}
