namespace MeusLegumes.Application.Contexts.Pacotes.Validations;

internal class CriarPacoteProdutoValidation : AbstractValidator<CriarPacoteProduto>
{
    public CriarPacoteProdutoValidation()
    {
        RuleFor(p => p.ProdutoId)
           .NotEqual(Guid.Empty).WithMessage("Informe o produto a ser adicionado no pacote");

        RuleFor(p => p.UnidadeId)
          .NotEqual(Guid.Empty).WithMessage("Informe a unidade do produto");

        RuleFor(p => p.Quantidade)
            .NotEmpty().WithMessage("Informe a quantidade do produto")
            .GreaterThan(0).WithMessage("A quantidade do produto deve ser maior que zero (0)");
    }
    
}
