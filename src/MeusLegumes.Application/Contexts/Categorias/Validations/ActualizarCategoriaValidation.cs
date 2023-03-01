namespace MeusLegumes.Application.Contexts.Categorias.Validations
{
    internal class ActualizarCategoriaValidation : AbstractValidator<ActualizarCategoria>
    {
        public ActualizarCategoriaValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("O Id da categoria é inválido");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("O nome da categoria deve ser informado")
                .MinimumLength(4).WithMessage("O nome da categoria deve ter no minimo 4 caracteres");
        }
    }
}
