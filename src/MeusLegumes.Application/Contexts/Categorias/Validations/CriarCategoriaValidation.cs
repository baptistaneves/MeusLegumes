namespace MeusLegumes.Application.Contexts.Categorias.Validations
{
    internal class CriarCategoriaValidation: AbstractValidator<CriarCategoria>
    {
        public CriarCategoriaValidation()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("O nome da categoria deve ser informado")
                .MinimumLength(4).WithMessage("O nome da categoria deve ter no minimo 4 caracteres");
        }
    }
}
