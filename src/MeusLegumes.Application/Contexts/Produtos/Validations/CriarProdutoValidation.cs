namespace MeusLegumes.Application.Contexts.Produtos.Validations;

internal class CriarProdutoValidation : AbstractValidator<CriarProduto>
{
	public CriarProdutoValidation()
	{
		RuleFor(p => p.Nome)
			.NotEmpty().WithMessage("Informe o nome");

        RuleFor(p => p.PrecoUnitario)
            .NotEmpty().WithMessage("Informe o preço unitário do produto")
            .GreaterThan(20).WithMessage("O valor do preço do produto deve ser maior que vinte (20)");

        RuleFor(p => p.UrlImagemPrincipal)
            .NotEmpty().WithMessage("Selecione uma imagem para o produto");

        RuleFor(p => p.CategoriaId)
            .NotEqual(Guid.Empty).WithMessage("A categoria não foi informada ou o seu Id é inválido");

        RuleFor(p => p.MotivoId)
            .NotEqual(Guid.Empty).WithMessage("O motivo não foi informado ou o seu Id é inválido");

        RuleFor(p => p.UnidadeId)
            .NotEqual(Guid.Empty).WithMessage("A unidade não foi informada ou o seu Id é inválido");

        RuleFor(p => p.ImpostoId)
            .NotEqual(Guid.Empty).WithMessage("O imposto não foi informado ou o seu Id é inválido");

        When(p => p.EmPromocao == true, () =>
        {
            RuleFor(p => p.PrecoPromocional)
           .NotEmpty().WithMessage("Informe o preço promocional do produto")
           .GreaterThan(20).WithMessage("O valor do preço promocional deve ser maior que vinte (20)");
        });
    }
}
